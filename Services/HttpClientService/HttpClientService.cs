using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace ForwardWebhook.Services.HttpClientService
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpClientService> _logger;

        public HttpClientService(HttpClient httpClient, ILogger<HttpClientService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object body)
        {
            try
            {
                var json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);
                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException($"Request to {url} failed: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new ApplicationException($"Deserialization error: {ex.Message}", ex);
            }
        }
    }
}
