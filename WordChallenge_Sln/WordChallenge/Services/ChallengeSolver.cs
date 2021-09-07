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
        private IWordDictionaryCache wordCache;

        public IReadOnlyList<string> Solve(string startWord, string targetWord, IWordDictionaryCache wordCache)
        {
            this.wordCache = wordCache;

            var initialWordPair = new WordPair(startWord, targetWord);

            var finalWordPair = this.PopulateWordPairs(initialWordPair);

            if (finalWordPair == null)
            {
                // Handle Unsolvable
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

                    if (tryPair.UnmatchedCharacterCount < input.UnmatchedCharacterCount)
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
                    tryPair.SetParent(input);
                    tryInnerPair.SetParent(tryPair);

                    return tryInnerPair;
                }
            }

            return null;
        }
    }
}
