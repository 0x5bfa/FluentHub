using System.Threading.Tasks;

namespace FluentHub.Services
{
    public interface ILogWriter
    {
        Task InitializeAsync(string name);
        Task WriteLineToLogAsync(string text);
        void WriteLineToLog(string text);
    }
}