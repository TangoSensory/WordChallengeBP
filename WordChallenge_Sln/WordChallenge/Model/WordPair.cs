namespace WordChallenge.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WordChallenge.Helpers;

    public sealed class WordPair
    {
        private readonly WordMatchResult wordMatchResult;

        public WordPair(string startWord, string targetWord)
        {
            this.StartWord = startWord;
            this.TargetWord = targetWord;
            this.wordMatchResult = WordHelper.GetUnmatchedCharacters(this.StartWord, this.TargetWord);

            if (UnmatchedCharacterCount < 0)
            {
                // Log here if required
                throw new Exception("Invalid Word Format - word lengths do not match");
            }
        }

        public void SetPreviousWords(IList<string> previousStartWords, IList<string> previousTargetWords)
        {
            this.PreviousStartWords = previousStartWords;
            this.PreviousTargetWords = previousTargetWords;
        }

        public void SetParent(WordPair parent)
        {
            this.ParentWordPair = parent;
        }

        public bool IsTopLevel => this.PreviousStartWords?.Any() != true;
        public string StartWord { get; }
        public string TargetWord { get; }
        public IList<string> PreviousStartWords { get; private set;  }
        public IList<string> PreviousTargetWords { get; private set; }

        public IList<string> InnerStartWordOptions { get; set; }
        public IList<string> InnerTargetWordOptions { get; set; }

        public int UnmatchedCharacterCount => this.wordMatchResult?.UnmatchedCharacterCount ?? -1;

        public bool AreAdjacentOrSame => UnmatchedCharacterCount >= 0 && this.UnmatchedCharacterCount <= 1;
        public bool AreDifferentWords => UnmatchedCharacterCount > 0;

        public IList<int> MatchedCharacterLocations => this.wordMatchResult?.MatchedCharacterLocations;

        public WordPair ParentWordPair { get; private set; }

        public IEnumerable<string> ReturnStartWordChangeHistory()
        {
            var output = new List<string> { this.StartWord };

            if (this.IsTopLevel)
            {
                return output;
            }

            return this.ParentWordPair.ReturnStartWordChangeHistory().Concat(output);
        }

        public IEnumerable<string> ReturnTargetWordChangeHistory()
        {
            var output = new List<string>();

            if (this.AreDifferentWords)
            {
                output.Add(this.TargetWord);
            }

            if (this.IsTopLevel)
            {
                return output;
            }

            return output.Concat(this.ParentWordPair.ReturnStartWordChangeHistory());
        }

        public IEnumerable<string> ReturnWordChangeHistory()
        {
            return this.ReturnStartWordChangeHistory().Concat(this.ReturnTargetWordChangeHistory());
        }
    }
}
