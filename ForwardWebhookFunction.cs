using System.Text.Json;
using Azure.Messaging.ServiceBus;
using ForwardWebhook.Models;
using ForwardWebhook.Services.HttpClientService;
using ForwardWebhook.Services.UrlService;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ForwardWebhook
{
    public class ForwardWebhookFunction
    {
        private readonly ILogger<ForwardWebhookFunction> _logger;
        private readonly IHttpClientService _httpClientService;
        private readonly IUrlService _urlService;
        public ForwardWebhookFunction(
            ILogger<ForwardWebhookFunction> logger,
            IHttpClientService HttpClientService,
            IUrlService UrlService)
        {
            _logger = logger;
            _httpClientService = HttpClientService;
            _urlService = UrlService;
        }

        [Function(nameof(ForwardWebhookFunction))]
        public async Task Run(
        [ServiceBusTrigger("QueueName", Connection = "ConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
        {
            try
            {
                _logger.LogInformation("Iniciando processamento da mensagem {MessageId}", message.MessageId);

                string jsonString = message.Body.ToString();
                ForwardWebhookDTO? body = JsonSerializer.Deserialize<ForwardWebhookDTO>(jsonString);

                if (body == null || body.Body == null)
                {
                    _logger.LogError("O body buscado na fila não pode ser null. MessageId: {MessageId}", message.MessageId);
                    throw new Exception("O body buscado na fila não pode ser null");
                }

                string url = _urlService.SearchUrlForSending(body.SupplierIdentifier);
                var result = await _httpClientService.PostAsync(url, body);

                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Requisição HTTP realizada com sucesso. Finalizando mensagem {MessageId}", message.MessageId);
                    await messageActions.CompleteMessageAsync(message);
                }
                else
                {
                    _logger.LogError("Erro na requisição HTTP. StatusCode: {StatusCode}. MessageId: {MessageId}", result.StatusCode, message.MessageId);
                    throw new Exception($"Houve um erro na requisição, Status Code {result.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar a mensagem da fila. MessageId: {MessageId}", message.MessageId);
                throw;
            }
        }

    }
}
