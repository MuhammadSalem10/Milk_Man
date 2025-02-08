using MilkMan.Shared.Interfaces;
using Serilog;


namespace MilkMan.Infrastructure.Services
{
    public class LoggerManager : ILoggerManager
    {
        private readonly ILogger _logger;

        public LoggerManager()
        {
            _logger = Log.ForContext<LoggerManager>();
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }
        public void Information(string message)
        {
            _logger.Information(message);
        }

        public void Warning(string message)
        {
            _logger.Warning(message);
        }


        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Fatal(string message)
        {
           _logger.Fatal(message);
        }
    }
}
