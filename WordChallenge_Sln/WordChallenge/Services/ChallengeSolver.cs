namespace WordChallenge.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using WordChallenge.Cache.Interfaces;
    using WordChallenge.Model;
    using WordChallenge.Services.Interfaces;

    public class ChallengeSolver : IChallengeSolver
    {
        private readonly IWordDictionaryCache wordCache;
        private readonly IErrorHandlerService errorHandlerService;
        private readonly IDataReaderService dataReaderService;

        public bool IsInitialised { get; private set; }

        public ChallengeSolver(IWordDictionaryCache wordCache, IErrorHandlerService errorHandlerService, IDataReaderService dataReaderService)
        {
            this.wordCache = wordCache;
            this.errorHandlerService = errorHandlerService;
            this.dataReaderService = dataReaderService;
        }

        public bool Initialise(string dictPath, string outFilePath)
        {
            if (!this.dataReaderService.CheckReaderSourceExists(dictPath))
            {
                this.errorHandlerService.HandleError("Error: Dictionary file not found");
                return false;
            }

            string inputBlob;
            try
            {
                inputBlob = this.dataReaderService.ReadAll(dictPath);
            }
            catch (Exception ex)
            {
                this.errorHandlerService.HandleException(ex, "Error: Unable to read Dictionary data");
                return false;
            }

            if (string.IsNullOrEmpty(inputBlob))
            {
                this.errorHandlerService.HandleError("Error: Unable to read Dictionary data");
                return false;
            }

            if (!this.wordCache.LoadFilteredDictionary(inputBlob))
            {
                this.errorHandlerService.HandleError("Error: Unable to load Dictionary data");
                return false;
            }

            return this.wordCache.IsDataLoadComplete;
        }

        public IReadOnlyList<string> Solve(string startWord, string targetWord)
        {
            if (!this.wordCache.IsDataLoadComplete)
            {
                this.errorHandlerService.HandleError("Error: Dictionary not loaded");
                return null;
            }

            var initialWordPair = new WordPair(startWord, targetWord);

            var finalWordPair = this.PopulateWordPairs(initialWordPair);

            if (finalWordPair == null)
            {
                this.errorHandlerService.HandleError("No solution exists for the given words");
                return null;
            }

            return finalWordPair.ReturnWordChangeHistory().ToList();
        }

        private WordPair PopulateWordPairs(WordPair input)
        {
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
                    var tryPair = new WordPair(sw, tw);
                    if (tryPair.AreAdjacentOrSame)
                    {
                        tryPair.SetParent(input);
                        return tryPair;
                    }

                    if (tryPair.UnmatchedCharacterCount <= input.UnmatchedCharacterCount)
                    {
                        tryPairs.Add(tryPair);
                    }
                }
            }

            foreach (var tryPair in tryPairs.OrderBy(x => x.UnmatchedCharacterCount))
            {
                var tryInnerPair = this.PopulateWordPairs(tryPair);
                if (tryInnerPair != null) 
                {
                    tryInnerPair.SetParent(input);

                    return tryInnerPair;
                }
            }

            return null;
        }
    }
}
