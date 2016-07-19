using System;

namespace Api
{
    public class ErrorHandler : IErrorHandler
    {
        public void LogError(Exception exception)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
        }
    }
}