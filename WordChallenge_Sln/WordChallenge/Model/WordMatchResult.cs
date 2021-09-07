namespace WordChallenge.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WordMatchResult
    {
        public WordMatchResult(List<int> matchedCharacterLocations, int wordLength)
        {
            this.wordLength = wordLength;
            this.MatchedCharacterLocations = matchedCharacterLocations ?? new List<int>();
        }

        private int wordLength;
        public int MatchCount => this.MatchedCharacterLocations?.Count ?? -1;
        public int UnmatchedCharacterCount
        {
            get
            {
                if (this.MatchedCharacterLocations == null)
                {
                    return -1;
                }

                return this.wordLength - this.MatchedCharacterLocations.Count;
            }
        }

        public IList<int> MatchedCharacterLocations { get; private set; }
    }
}
