namespace WordChallenge.Helpers
{
    using System.Collections.Generic;
    using WordChallenge.Model;

    public static class WordHelper
    {
        public static WordMatchResult GetUnmatchedCharacters(string word1, string word2)
        {
            if (word1.Length != word2.Length)
            {
                return null;
            }

            var upper1 = word1.ToUpper();
            var upper2 = word2.ToUpper();

            var outList = new List<int>();
            for (int i = 0; i < word1.Length; i++)
            {
                if (upper1[i] == upper2[i])
                {
                    outList.Add(i);
                }
            }

            return new WordMatchResult(word1, word2, outList, Globals.Constants.WordLength);
        }
    }
}
