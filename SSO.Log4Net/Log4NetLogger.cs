using log4net.Config;
using SSO.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SSO.Log4Net
{
    public class Log4NetLogger : ILogger
    {
        private readonly log4net.ILog _log;
        private const string Long4NetConfigFileName = "log4net.config";

        public Log4NetLogger()
        {
            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var repo = log4net.LogManager.GetRepository(currentAssembly);
           // XmlConfigurator.Configure(repo, new FileInfo(Path.Combine(/*Path.GetDirectoryName(currentAssembly.Location)*/@"C:\Users\Mutaz\source\repos\SingleSignOnAssignment\SSO.WCF.Service\bin", Long4NetConfigFileName)));
            XmlConfigurator.Configure(repo, new FileInfo(Path.Combine(SSO.Common.StaticMembers.ApplicationDirectory, Long4NetConfigFileName)));

            _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void Error(Exception ex, string message)
        {
            _log.Error(message, ex);
        }

        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Warning(string message)
        {
            _log.Warn(message);
        }

        public Task InfoAsync(string message)
        {
            return Task.Run(() => _log.Info(message));
        }

        public Task WarningAsync(string message)
        {
            return Task.Run(() => _log.Warn(message));
        }

        public Task ErrorAsync(Exception ex, string message)
        {
            return Task.Run(() => _log.Error(message, ex));
        }
    }
}
