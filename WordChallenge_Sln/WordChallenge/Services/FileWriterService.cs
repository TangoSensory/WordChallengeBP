namespace WordChallenge.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WordChallenge.Services.Interfaces;

    public class FileWriterService : IDataWriterService
    {
        public bool CreateOutptTarget(string path)
        {
            throw new NotImplementedException();
        }

        public bool WriteLines(IReadOnlyList<string> lines)
        {
            throw new NotImplementedException();
        }
    }
}
