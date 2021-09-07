namespace WordChallenge.Cache.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WordChallenge.Services.Interfaces;

    public interface IWordDictionaryCache
    {
        bool LoadFilteredDictionary(IDataReaderService dataReaderService, int wordLength = -1);
        IList<string> GetPotentialWordChanges(string fromWord, IList<int> excludeCharacters = null, IList<string> excludeWords = null, int sortChar = -1);
    }
}
