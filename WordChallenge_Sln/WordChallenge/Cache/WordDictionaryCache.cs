namespace WordChallenge.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WordChallenge.Cache.Interfaces;
    using WordChallenge.Helpers;
    using WordChallenge.Model;
    using WordChallenge.Services.Interfaces;

    public class WordDictionaryCache : IWordDictionaryCache
    {
        private Dictionary<int, List<string>> SortedDictionaryCache = new Dictionary<int, List<string>>();
        private int wordLength;
        private readonly IDataReaderService dataReaderService;
        private readonly IErrorHandlerService errorHandlerService;

        public WordDictionaryCache(IDataReaderService dataReaderService, IErrorHandlerService errorHandlerService)
        {
            this.errorHandlerService = errorHandlerService;
            this.dataReaderService = dataReaderService;
        }

        public bool IsDataLoadComplete { get; set; }

        public bool ETL(string dictPath)
        {
            if (!this.dataReaderService.CheckReaderSourceExists(dictPath))
            {
                this.errorHandlerService.HandleError("Error: Dictionary file not found");
                return false;
            }

            string[] inputArray;
            try
            {
                inputArray = this.dataReaderService.ReadAll(dictPath);
            }
            catch (Exception ex)
            {
                this.errorHandlerService.HandleException(ex, "Error: Unable to read Dictionary data");
                return false;
            }

            if (inputArray?.Any() != true)
            {
                this.errorHandlerService.HandleError("Error: Unable to read Dictionary data");
                return false;
            }

            if (!this.BuildFilteredDictionary(inputArray))
            {
                this.errorHandlerService.HandleError("Error: Unable to load Dictionary data");
                return false;
            }

            return this.IsDataLoadComplete;
        }

        public IList<string> GetPotentialWordChanges(string fromWord, IList<int> charactersAlreadyMatched = null, IList<string> previousWords = null, int sortChar = -1)
        {
            var output = new List<string>();
            if (!this.IsDataLoadComplete)
            {
                // Log error if required
                return null;
            }

            if (fromWord.Length != this.wordLength)
            {
                // Log error if required
                return null;
            }

            sortChar = sortChar == -1 || sortChar >= this.wordLength ? 0 : sortChar;

            if (!this.SortedDictionaryCache.ContainsKey(sortChar))
            {
                this.SortedDictionaryCache[sortChar] = this.SortedDictionaryCache[0].OrderBy(x => x[sortChar]).ToList();
            }

            var matches = this.FindAllPotentialWords(fromWord, sortChar);

            if (matches?.Any() == true)
            {
                if (charactersAlreadyMatched?.Any() == true)
                {
                    // Prioritise words whose character swaps are not characters that are already matched
                    matches = matches.OrderBy(x => x.MatchedCharacterLocations.Except(charactersAlreadyMatched).Count());
                }

                if (previousWords?.Any() == true)
                {
                    // Remove words that have been used previously (to avoid back-tracking)

                    // NB* Potential Tweak:
                    // Changing the Where() to an OrderBy(), would include previously used words, but de-prioritise
                    // them. Because Word-Pairs are used, and the way the work, it's possible that a solution is
                    // only feasible with some back tracking either at the start or the end. These could be filtered
                    // out of the final word list if necessary
                    matches = matches.Where(x => !previousWords.Contains(x.Word2));
                }

                output = matches.Select(x => x.Word2).ToList();
            }

            return output;
        }

        private bool BuildFilteredDictionary(string[] inputArray, int wordLength = -1)
        {
            this.SortedDictionaryCache = new Dictionary<int, List<string>>();
            this.IsDataLoadComplete = false;

            var result = new List<string>();

            this.wordLength = wordLength == -1 ? Globals.Constants.WordLength : wordLength;

            for (int i = 0; i < inputArray.Length; i++)
            {
                if (inputArray[i].Length == this.wordLength)
                {
                    result.Add(inputArray[i]);
                }
            }

            if (result.Any())
            {
                this.IsDataLoadComplete = true;
                this.SortedDictionaryCache[0] = result.OrderBy(x => x).ToList();
            }

            return this.IsDataLoadComplete;
        }

        private IEnumerable<WordMatchResult> FindAllPotentialWords(string fromWord, int sortChar)
        {
            return this.SortedDictionaryCache[sortChar]
                .Select(x => WordHelper.GetUnmatchedCharacters(fromWord, x))
                .Where(x => x.UnmatchedCharacterCount == 1);
        }
    }
}
