namespace WordChallenge.Cache.Interfaces
{
    using System.Collections.Generic;

    public interface IWordDictionaryCache
    {
        bool IsDataLoadComplete { get; }
        bool ETL(string dictPath);
        IList<string> GetPotentialWordChanges(string fromWord, IList<int> matchedCharacters = null, IList<string> previousWords = null, int sortChar = -1);
    }
}
