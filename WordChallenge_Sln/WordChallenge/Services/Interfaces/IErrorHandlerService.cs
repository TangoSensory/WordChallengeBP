namespace WordChallenge.Services.Interfaces
{
    using System;

    public interface IErrorHandlerService
    {
        void HandleError(string errorMessage, bool notifyUser = true);
        void HandleException(Exception ex, string additionalErrorMessage = null, bool notifyUser = true);
    }
}
