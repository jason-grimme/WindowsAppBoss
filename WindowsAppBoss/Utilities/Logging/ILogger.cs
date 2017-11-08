using System;

namespace WindowsAppBoss.Utilities.Logging
{
    public interface ILogger
    {
        void Log(Logger.LogSeverity severity, string message);
        void Log(Exception ex, string message);
    }
}