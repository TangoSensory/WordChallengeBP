namespace WordChallenge.Services
{
    using System;
    using System.IO;
    using WordChallenge.Services.Interfaces;

    public class FileReaderService : IDataReaderService
    {
        public bool CheckReaderSourceExists(string path)
        {
            var result = false;

            try
            {
                result = File.Exists(path);
                return result;
            }
            catch (Exception)
            {
                //Log here if required
            }

            return false;
        }

        public string[] ReadAll(string path)
        {
            string[] result = null;

            try
            {
                result = File.ReadAllLines(path);
            }
            catch (Exception)
            {
                //Log here if required
            }

            return result;
        }
    }
}
