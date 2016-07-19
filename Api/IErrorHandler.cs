using System;

namespace Api
{
    public interface IErrorHandler
    {
        void LogError(Exception exception);
    }
}
