namespace WordChallenge.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WordChallenge.Cache.Interfaces;

    public interface IChallengeSolver
    {
        bool Initialise(string dictPath, string outFilePath);
        IReadOnlyList<string> Solve(string startWord, string targetWord);
    }
}
