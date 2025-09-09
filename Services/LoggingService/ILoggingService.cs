using System;

namespace ForwardWebhook.Services.LoggingService
{
    public interface ILoggingService
    {
        string GenerateCorrelationId();

        void LogError(string message, string correlationId, string messageId, Exception? ex = null);
        void LogInformation(string message, string correlationId, string messageId);
        void LogWarning(string message, string correlationId, string messageId);
    }
}
