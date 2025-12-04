using ForwardWebhook.Consts;
using Microsoft.Extensions.Configuration;

namespace ForwardWebhook.Services.UrlService
{
    public class UrlService : IUrlService
    {
        private readonly IConfiguration _configuration;

        public UrlService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SearchUrlForSending(string destinationIdentifier)
        {
            string baseUrl = _configuration["BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("BaseUrl não configurada no local.settings.json");

            return destinationIdentifier switch
            {
                ProviderIdentifiers.QiFgts => $"{baseUrl}/Qi/WebHook?type=0",
                ProviderIdentifiers.QiInss => $"{baseUrl}/Qi/Webhook?type=1",
                ProviderIdentifiers.QiInstallments => $"{baseUrl}/Qi/Webhook?type=2",
                ProviderIdentifiers.UnicoCheck => $"{baseUrl}/UnicoCheck/Update-Process",
                ProviderIdentifiers.UnicoId => $"{baseUrl}/UnicoCheck/Update-Id-Unico",
                ProviderIdentifiers.Bmp => $"{baseUrl}/Bmp/WebHook",
                ProviderIdentifiers.BrasilIndoc => $"{baseUrl}/Documentoscopia/WebHook",
                ProviderIdentifiers.Iugu => $"{baseUrl}/Iugu/WebHook-Iugu",
                ProviderIdentifiers.J17Operation => $"{baseUrl}/J17/Web-Hook",
                ProviderIdentifiers.J17Simulation => $"{baseUrl}/J17/Simulation-WebHook",
                ProviderIdentifiers.Nuvideo => $"{baseUrl}/Operation/Web-Hook-Nuvideo",
                ProviderIdentifiers.NycBank => $"{baseUrl}/NYC/WebHook",
                ProviderIdentifiers.OneBlink => $"{baseUrl}/OneBlink/Webhook-One-Blink",
                ProviderIdentifiers.ParanaBank => $"{baseUrl}/ParanaBank/Webhook",

                _ => throw new Exception($"Url não encontrada para o parâmetro '{destinationIdentifier}'")
            };
        }
    }
}
