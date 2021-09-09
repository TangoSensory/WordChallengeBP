namespace WordChallenge.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WordChallenge.Cache.Interfaces;
    using WordChallenge.Model;
    using WordChallenge.Services.Interfaces;

    public class ChallengeSolver : IChallengeSolver
    {
        private readonly IWordDictionaryCache wordCache;
        private readonly IErrorHandlerService errorHandlerService;
        private int attempts;

        public bool IsInitialised { get; private set; }

        public ChallengeSolver(IWordDictionaryCache wordCache, IErrorHandlerService errorHandlerService)
        {
            this.wordCache = wordCache;
            this.errorHandlerService = errorHandlerService;
        }

        public WordPair Solve(string startWord, string targetWord)
        {
            this.attempts = 0;

            if (!this.wordCache.IsDataLoadComplete)
            {
                this.errorHandlerService.HandleError("Error: Dictionary not loaded");
                return null;
            }

            WordPair initialWordPair = null;
            try
            {
                initialWordPair = new WordPair(startWord, targetWord);
            }
            catch (Exception ex)
            {
                // NB Because the params are already validated, this scenario should never happen
                this.errorHandlerService.HandleException(ex, $"Word lengths don't match: {startWord}, {targetWord}");
                return null;
            }

            var finalWordPair = this.PopulateWordPairs(initialWordPair);

            if (finalWordPair == null)
            {
                // Is this an error, or a valid result?
                this.errorHandlerService.HandleError("Unable to derive a solution for the given words");
                return null;
            }

            return finalWordPair;
        }

        private WordPair PopulateWordPairs(WordPair input)
        {
            if (this.attempts++ > Globals.Constants.MaximumPathsToTry)
            {
                return null;
            }

            if (input.Depth > Globals.Constants.MaximumPathDepth)
            {
                return null;
            }

            if (input.AreAdjacentOrSame)
            {
                return input;
            }

            var nextStartWordOptions = this.wordCache.GetPotentialWordChanges(input.StartWord, input.MatchedCharacterLocations, input.PreviousStartWords);
            var nextTargetWordOptions = this.wordCache.GetPotentialWordChanges(input.TargetWord, input.MatchedCharacterLocations, input.PreviousTargetWords);

            var tryPairs = new List<WordPair>();
            foreach (var sw in nextStartWordOptions)
            {
                foreach (var tw in nextTargetWordOptions)
                {
                    WordPair tryPair = null;
                    try
                    {
                        tryPair = new WordPair(sw, tw);
                    }
                    catch (Exception ex)
                    {
                        // NB Because the dictionary words are filtered on loading, this scenario should never happen
                        this.errorHandlerService.HandleException(ex, $"Word lengths don't match: {sw}, {tw}", notifyUser:false);
                        continue;
                    }

                    if (tryPair.AreAdjacentOrSame)
                    {
                        tryPair.SetParent(input);
                        return tryPair;
                    }

                    tryPairs.Add(tryPair);
                }
            }

            foreach (var tryPair in tryPairs.OrderBy(x => x.UnmatchedCharacterCount))
            {
                tryPair.SetParent(input);
                var tryInnerPair = this.PopulateWordPairs(tryPair);
                if (tryInnerPair != null) 
                {
                    return tryInnerPair;
                }
            }

            return null;
        }
    }
}
