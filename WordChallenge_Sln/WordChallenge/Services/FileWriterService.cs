namespace WordChallenge.Services
{
    using System;
    using System.IO;
    using WordChallenge.Services.Interfaces;

    public class FileWriterService : IDataWriterService
    {
        private string path;
        private readonly IErrorHandlerService errorHandlerService;

        public FileWriterService(IErrorHandlerService errorHandlerService)
        {
            this.errorHandlerService = errorHandlerService;
        }

        public bool CreateOutptTarget(string path)
        {
            if (File.Exists(path))
            {
                this.errorHandlerService.HandleError("Error: Unable to create output file as the file already exists");
                return false;
            }

            try
            {
                File.Create(path).Dispose();

                this.path = path;
                return true;
            }
            catch (Exception ex)
            {
                this.errorHandlerService.HandleException(ex, "Error: Unable to create output file");
            }

            return false;
        }

        public bool Write(string outputText)
        {
            if (string.IsNullOrEmpty(this.path) || !File.Exists(this.path))
            {
                this.errorHandlerService.HandleError("Error: Output file not found");
                return false;
            }

            try
            {
                using StreamWriter output = new StreamWriter(this.path);
                output.Write(outputText);
            }
            catch (Exception ex)
            {
                this.errorHandlerService.HandleException(ex, "Error: Unable to create output file");
                return false;
            }

            return true;
        }
    }
}
