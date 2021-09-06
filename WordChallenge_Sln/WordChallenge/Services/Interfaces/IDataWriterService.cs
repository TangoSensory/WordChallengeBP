namespace WordChallenge.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IDataWriterService
    {
        bool CreateOutptTarget(string path);
        bool WriteLines(IReadOnlyList<string> lines);
    }
}
