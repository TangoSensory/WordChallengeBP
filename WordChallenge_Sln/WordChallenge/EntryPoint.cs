namespace WordChallenge
{
    using System;
    using WordChallenge.Cache.Interfaces;
    using WordChallenge.Services.Interfaces;
    using WordChallenge.Validators.Interfaces;

    public class EntryPoint
    {
        private readonly IParamsValidator paramsValidator;
        private readonly IChallengeSolver challengeSolver;
        private readonly IErrorHandlerService errorHandlerService;
        private readonly IDataWriterService dataWriterService;
        private readonly IWordDictionaryCache wordCache;

        public EntryPoint(
            IParamsValidator paramsValidator, 
            IChallengeSolver challengeSolver, 
            IErrorHandlerService errorHandlerService, 
            IDataReaderService dataReaderService,
            IDataWriterService dataWriterService,
            IWordDictionaryCache wordCache)
        {
            this.paramsValidator = paramsValidator;
            this.challengeSolver = challengeSolver;
            this.errorHandlerService = errorHandlerService;
            this.dataWriterService = dataWriterService;
            this.wordCache = wordCache;
        }

        public void Execute(string[] args)
        {
            if (args.Length != Globals.Constants.AppParameterCount)
            {
                this.errorHandlerService.HandleError("Invalid parameters. Please try again supplying the following command line parameers: \"startWord\" \"targetWord\" \"dictionaryFilePath\" \"outputFilePath\"");
                return;
            }

            var startWord = args[0];
            var targetWord = args[1];
            var dictFilePath = args[2];
            var outputFilePath = args[3];

            try
            {
                this.paramsValidator.ValidateFilenameFormat(dictFilePath);
                this.paramsValidator.ValidateFilenameFormat(outputFilePath);
            }
            catch (Exception ex)
            {
                this.errorHandlerService.HandleException(ex);
                return;
            }

            try
            {
                this.paramsValidator.ValidateWordParams(startWord, targetWord);
            }
            catch (Exception ex)
            {
                this.errorHandlerService.HandleException(ex);
                return;
            }

            if (!this.dataWriterService.CreateOutptTarget(outputFilePath))
            {
                this.errorHandlerService.HandleError("Unable to create the output file");
                return;
            }

            if (!this.wordCache.ETL(dictFilePath))
            {
                return;
            }

            var result = this.challengeSolver.Solve(startWord, targetWord);

            if (result != null)
            {
                try
                {
                    this.dataWriterService.Write(result.ToString());

                    // NB In a production system, there'd be another Interface here to abstract the actual output device and
                    // enable full testing of this method
                    Console.WriteLine($"Solution written to {outputFilePath}.");
                }
                catch (Exception ex)
                {
                    this.errorHandlerService.HandleException(ex, "Error writing to output file");
                }
            }
        }

    }
}
