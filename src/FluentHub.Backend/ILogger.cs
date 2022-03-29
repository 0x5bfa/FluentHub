using System;

namespace FluentHub.Backend
{
    public interface ILogger
    {
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Error(Exception ex);
        void Error(string message, Exception ex);
        void Fatal(string message);
        void Fatal(Exception ex);
        void Fatal(string message, Exception ex);
    }
}