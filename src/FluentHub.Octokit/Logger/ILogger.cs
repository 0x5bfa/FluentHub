using System;
using System.Runtime.CompilerServices;

namespace FluentHub.Octokit.Logger
{
    internal interface ILogger
    {
        void Info(string error, [CallerMemberName] string caller = "");
        void Info(string info, object obj, [CallerMemberName] string caller = "");
        void Info(Exception ex);
        void Info(Exception ex, string error = "", [CallerMemberName] string caller = "");

        void Warn(string error, [CallerMemberName] string caller = "");
        void Warn(Exception ex);
        void Warn(Exception ex, string error = "", [CallerMemberName] string caller = "");

        void Error(string error, [CallerMemberName] string caller = "");
        void Error(Exception ex, string error = "", [CallerMemberName] string caller = "");
        void Error(Exception ex);

        void UnhandledError(Exception ex, string error = "", [CallerMemberName] string caller = "");
    }
}
