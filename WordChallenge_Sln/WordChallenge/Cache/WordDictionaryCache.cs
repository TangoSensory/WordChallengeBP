﻿namespace WordChallenge.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WordChallenge.Cache.Interfaces;
    using WordChallenge.Services.Interfaces;

    public class WordDictionaryCache : IWordDictionaryCache
    {
        public bool IsDataLoadComplete => throw new NotImplementedException();

        public IList<string> GetPotentialWordChanges(string fromWord, IList<int> excludeCharacters = null, IList<string> excludeWords = null, int sortChar = -1)
        {
            throw new NotImplementedException();
        }

        public bool LoadFilteredDictionary(string inputBlob, int wordLength = -1)
        {
            throw new NotImplementedException();
        }
    }
}
