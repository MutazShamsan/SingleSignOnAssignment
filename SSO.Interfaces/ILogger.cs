using System;
using System.Threading.Tasks;

namespace SSO.Interfaces
{
    public interface ILogger
    {
        void Info(string message);
        void Warning(string message);
        void Error(Exception ex, string message);

        Task InfoAsync(string message);
        Task WarningAsync(string message);
        Task ErrorAsync(Exception ex, string message);
    }
}
