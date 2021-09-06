namespace WordChallenge.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WordChallenge.Helpers;

    public sealed class WordPair
    {
        private readonly WordMatchResult wordMatchResult;

        public WordPair(string startWord, string targetWord, IReadOnlyList<string> previousStartWords = null, IReadOnlyList<string> previousTargetWords = null)
        {
            this.StartWord = startWord;
            this.TargetWord = targetWord;
            this.PreviousStartWords = previousStartWords;
            this.PreviousTargetWords = previousTargetWords;
            this.wordMatchResult = WordHelper.GetUnmatchedCharacters(this.StartWord, this.TargetWord);

            if (UnmatchedCharacterCount < 0)
            {
                // Log here if required
                throw new Exception("Invalid Word Format - word lengths do not match");
            }
        }

        public bool IsTopLevel => this.PreviousStartWords?.Any() != true;
        public string StartWord { get; }
        public string TargetWord { get; }
        public IReadOnlyList<string> PreviousStartWords { get; }
        public IReadOnlyList<string> PreviousTargetWords { get; }

        public IList<string> InnerStartWordOptions { get; set; }
        public IList<string> InnerTargetWordOptions { get; set; }

        public int UnmatchedCharacterCount => this.wordMatchResult?.UnmatchedCharacterCount ?? -1;

        public bool AreAdjacentOrSame => UnmatchedCharacterCount >= 0 && this.UnmatchedCharacterCount <= 1;
        public bool AreSame => UnmatchedCharacterCount == 0;

        public IReadOnlyList<int> MatchedCharacterLocations => this.wordMatchResult?.MatchedCharacterLocations;

        public WordPair InnerWordPair { get; set; }
    }
}
