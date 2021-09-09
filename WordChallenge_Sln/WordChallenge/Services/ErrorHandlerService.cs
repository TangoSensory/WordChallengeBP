namespace WordChallenge.Services
{
    using System;
    using WordChallenge.Extensions;
    using WordChallenge.Services.Interfaces;

    public class ErrorHandlerService : IErrorHandlerService
    {
        public void HandleError(string errorMessage, bool notifyUser = true)
        {
            if (notifyUser)
            {
                // NB In a production system, there'd be another Interface here to abstract the actual output device and
                // enable full testing of this method
                Console.WriteLine(errorMessage);
            }
        }

        public void HandleException(Exception ex, string additionalErrorMessage = null, bool notifyUser = true)
        {
            if (notifyUser)
            {
                // NB In a production system, there'd be another Interface here to abstract the actual output device and
                // enable full testing of this method
                Console.WriteLine(additionalErrorMessage);
                Console.WriteLine(ex.FullString());
            }
        }
    }
}
