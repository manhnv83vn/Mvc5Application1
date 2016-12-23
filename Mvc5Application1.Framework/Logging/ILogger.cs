using System;

namespace Mvc5Application1.Framework.Logging
{
    public interface ILogger
    {
        void LogInformational(string message);

        void LogWarning(string message);

        void LogWarning(Exception ex);

        void LogError(Exception ex);

        void LogUnHandleError(Exception ex);
    }
}