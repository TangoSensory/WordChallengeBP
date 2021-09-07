namespace WordChallenge.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WordChallenge.Services.Interfaces;

    public class ErrorHandlerService : IErrorHandlerService
    {
        public void HandleError(string errorMessage, bool notifyUser = true)
        {
            throw new NotImplementedException();
        }

        public void HandleException(Exception ex, string additionalErrorMessage = null, bool notifyUser = true)
        {
            throw new NotImplementedException();
        }
    }
}
