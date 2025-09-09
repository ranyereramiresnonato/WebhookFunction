using Microsoft.Extensions.Logging;

namespace ForwardWebhook.Services.LoggingService
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger<LoggingService> _logger;

        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
        }

        public string GenerateCorrelationId()
        {
            return Guid.NewGuid().ToString();
        }

        public void LogError(string message, string correlationId, string messageId, Exception? ex = null)
        {
            if (ex != null)
            {
                _logger.LogError(ex, "[CorrelationId: {CorrelationId}] [MessageId: {MessageId}] {Message}", correlationId, messageId, message);
            }
            else
            {
                _logger.LogError("[CorrelationId: {CorrelationId}] [MessageId: {MessageId}] {Message}", correlationId, messageId, message);
            }
        }

        public void LogInformation(string message, string correlationId, string messageId)
        {
            _logger.LogInformation("[CorrelationId: {CorrelationId}] [MessageId: {MessageId}] {Message}", correlationId, messageId, message);
        }

        public void LogWarning(string message, string correlationId, string messageId)
        {
            _logger.LogWarning("[CorrelationId: {CorrelationId}] [MessageId: {MessageId}] {Message}", correlationId, messageId, message);
        }
    }
}
