namespace WordChallenge
{
    using System;
    using WordChallenge.Services.Interfaces;
    using WordChallenge.Validators.Interfaces;

    public class EntryPoint
    {
        private readonly IParamsValidator paramsValidator;
        private readonly IChallengeSolver challengeSolver;
        private readonly IErrorHandlerService errorHandlerService;

        public EntryPoint(IParamsValidator paramsValidator, IChallengeSolver challengeSolver, IErrorHandlerService errorHandlerService)
        {
            this.paramsValidator = paramsValidator;
            this.challengeSolver = challengeSolver;
            this.errorHandlerService = errorHandlerService;
        }

        public void Execute(string[] args)
        {
            if (args.Length != Globals.Constants.AppParameterCount)
            {
                //this.errorHandlerService
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
                //this.errorHandlerService
                return;
            }

            try
            {
                this.paramsValidator.ValidateWordParams(startWord, targetWord);
            }
            catch (Exception ex)
            {
                //this.errorHandlerService
                return;
            }


        }
    }
}
