namespace WordChallenge.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IDataReaderService
    {
        bool CheckReaderSourceExists(string path);
        string ReadAll(string path);
    }
}
