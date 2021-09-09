namespace WordChallenge.Model
{
    using System.Collections.Generic;

    public class WordMatchResult
    {
        public WordMatchResult(string word1, string word2, List<int> matchedCharacterLocations, int wordLength)
        {
            this.Word1 = word1;
            this.Word2 = word2;
            this.wordLength = wordLength;
            this.MatchedCharacterLocations = matchedCharacterLocations ?? new List<int>();
        }

        private readonly int wordLength;

        public string Word1 { get; private set; }
        public string Word2 { get; private set; }
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
