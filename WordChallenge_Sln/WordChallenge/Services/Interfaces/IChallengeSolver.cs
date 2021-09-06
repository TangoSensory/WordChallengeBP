namespace WordChallenge.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WordChallenge.Cache.Interfaces;

    public interface IChallengeSolver
    {
        IReadOnlyList<string> Solve(string startWord, string targetWord, IWordDictionaryCache wordCache);
    }
}
