using System.Text.Json;
using Azure.Messaging.ServiceBus;
using ForwardWebhook.Models;
using ForwardWebhook.Services.HttpClientService;
using ForwardWebhook.Services.LoggingService;
using ForwardWebhook.Services.UrlService;
using Microsoft.Azure.Functions.Worker;

namespace ForwardWebhook
{
    public class ForwardWebhookFunction
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IUrlService _urlService;
        private readonly ILoggingService _loggingService;

        public ForwardWebhookFunction(
            IHttpClientService HttpClientService,
            IUrlService UrlService,
            ILoggingService LoggingService)
        {
            _httpClientService = HttpClientService;
            _urlService = UrlService;
            _loggingService = LoggingService;
        }

        [Function(nameof(ForwardWebhookFunction))]
        public async Task Run(
            [ServiceBusTrigger("QueueName", Connection = "ConnectionString")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            string correlationId = _loggingService.GenerateCorrelationId();
            string messageId = message.MessageId;
            try
            {
                string jsonString = message.Body.ToString();
                ForwardWebhookDTO? body = JsonSerializer.Deserialize<ForwardWebhookDTO>(jsonString);

                if (body == null || body.Payload == null)
                {
                    throw new Exception("O body buscado na fila não pode ser null");
                }

                string url = _urlService.SearchUrlForSending(body.DestinationIdentifier);

                Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "x-correlation-id", correlationId }
                };

                var result = await _httpClientService.PostAsync(url, body, headers);

                if (result.IsSuccessStatusCode)
                {
                    _loggingService.LogInformation("Mensagem enviada com sucesso", correlationId, messageId);
                    await messageActions.CompleteMessageAsync(message);
                }
                else
                {
                    string errorMsg = $"Houve um erro na requisição, Status Code {result.StatusCode}";
                    throw new Exception(errorMsg);
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Erro ao processar a mensagem", correlationId, messageId, ex);
                throw;
            }
        }
    }
}
