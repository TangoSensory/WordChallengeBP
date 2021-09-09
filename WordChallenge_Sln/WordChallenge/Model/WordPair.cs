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
                throw new Exception("Invalid Word Format - word lengths do not match");
            }
        }

        public int Depth => this.GetDepth(0);
        public bool IsTopLevel => this.PreviousStartWords?.Any() != true;
        public int UnmatchedCharacterCount => this.wordMatchResult?.UnmatchedCharacterCount ?? -1;
        public bool AreAdjacentOrSame => UnmatchedCharacterCount >= 0 && this.UnmatchedCharacterCount <= 1;
        public bool AreDifferentWords => UnmatchedCharacterCount > 0;

        public string StartWord { get; }
        public string TargetWord { get; }
        public IList<string> PreviousStartWords { get; private set; } = new List<string>();
        public IList<string> PreviousTargetWords { get; private set; } = new List<string>();

        public IList<int> MatchedCharacterLocations => this.wordMatchResult?.MatchedCharacterLocations;

        public WordPair ParentWordPair { get; private set; }

        public void SetPreviousWords(WordPair parent)
        {
            this.PreviousStartWords = parent.PreviousStartWords.Append(parent.StartWord).Distinct().ToList();
            this.PreviousTargetWords = parent.PreviousTargetWords.Append(parent.TargetWord).Distinct().ToList();
        }

        public int GetDepth(int currentDepth)
        {
            return this.IsTopLevel ? currentDepth : this.ParentWordPair.GetDepth(currentDepth + 1);
        }

        public void SetParent(WordPair parent)
        {
            this.SetPreviousWords(parent);
            if (this.ParentWordPair != null)
            {
                this.ParentWordPair.SetParent(parent);
            }
            else
            {
                this.ParentWordPair = parent;
            }
        }

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

            return output.Concat(this.ParentWordPair.ReturnTargetWordChangeHistory());
        }

        public IEnumerable<string> ReturnWordChangeHistory()
        {
            return this.ReturnStartWordChangeHistory().Concat(this.ReturnTargetWordChangeHistory());
        }

        public override string ToString()
        {
            return string.Join(",", this.ReturnWordChangeHistory());
        }
    }
}
