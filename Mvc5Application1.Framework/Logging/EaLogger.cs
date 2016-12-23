using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Configuration;

namespace Mvc5Application1.Framework.Logging
{
    public class EaLogger : ILogger
    {
        private readonly bool _writeDetailLog;

        public EaLogger()
        {
            if (ConfigurationManager.AppSettings["WriteDetailLog"] != null)
            {
                bool.TryParse(ConfigurationManager.AppSettings["WriteDetailLog"], out _writeDetailLog);
            }
            Logger.SetLogWriter(new LogWriterFactory().Create());
        }

        public void LogInformational(string message)
        {
            Logger.Write(message, "Information");
        }

        public void LogWarning(string message)
        {
            Logger.Write(message, "Warning");
        }

        public void LogWarning(Exception ex)
        {
            string messageToWriteLog = ex.GetBaseException().Message;
            if (_writeDetailLog)
            {
                messageToWriteLog = messageToWriteLog + "\r\n" + ex.StackTrace;
            }
            Logger.Write(messageToWriteLog, "Warning");
        }

        public void LogError(Exception ex)
        {
            string messageToWriteLog = ex.GetBaseException().Message;
            if (_writeDetailLog)
            {
                messageToWriteLog = messageToWriteLog + "\r\n" + ex.StackTrace;
            }
            Logger.Write(messageToWriteLog, "Error");
        }

        public void LogUnHandleError(Exception ex)
        {
            string messageToWriteLog = ex.GetBaseException().Message;
            if (_writeDetailLog)
            {
                messageToWriteLog = messageToWriteLog + "\r\n" + ex.StackTrace;
            }
            Logger.Write(messageToWriteLog, "UnHandleError");
        }
    }
}