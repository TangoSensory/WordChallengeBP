namespace WordChallenge.Validators.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IParamsValidator
    {
        void ValidateFilenameFormat(string path);
        void ValidateWordParams(string start, string target);
    }
}
