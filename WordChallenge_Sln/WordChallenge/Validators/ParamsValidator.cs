namespace WordChallenge.Validators
{
    using System;
    using System.IO;
    using WordChallenge.Validators.Interfaces;

    public class ParamsValidator : IParamsValidator
    {
        public void ValidateFilenameFormat(string path)
        {
            // This is a very simple check and not very thorough, because file paths can be absolute or relative
            // In this case, it's not too important because the file paths will be validated during the read / write operations
            if (!this.ContainsOnlyValidCharacter(path))
            {
                throw new ArgumentException($"Error: {path} contains invalid characters");
            }
        }

        public void ValidateWordParams(string start, string target)
        {
            if (start.Length != Globals.Constants.WordLength || target.Length != Globals.Constants.WordLength)
            {
                throw new ArgumentException($"Error: {start} and {target} must both be {Globals.Constants.WordLength} characters long");
            }
        }

        private bool ContainsOnlyValidCharacter(string path)
        {
            return path.IndexOfAny(Path.GetInvalidPathChars()) == -1; ;
        }
    }
}
