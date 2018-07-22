using NLog;
using System;

namespace Ticket.Utility.Logger
{
    public class SimpleLogger
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        public void Info(string message)
        {
            Logger.Info(message);
        }

        public void Warn(string message)
        {
            Logger.Warn(message);
        }

        public void Error(Exception exception)
        {
            Logger.Error(exception);
        }

        public void Fatal(string message)
        {
            Logger.Fatal(message);
        }
    }
}
