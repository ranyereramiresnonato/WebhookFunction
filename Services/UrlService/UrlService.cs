using ForwardWebhook.Enums;
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

        public string SearchUrlForSending(int supplierIdentifier)
        {
            string baseUrl = _configuration["BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("BaseUrl não configurada no local.settings.json");

            return supplierIdentifier switch
            {
                (int)SupplierEnum.QiSociedadeDeCredito => $"{baseUrl}/Qi/WebHook?type=0",
                (int)SupplierEnum.QiInss => $"{baseUrl}/Qi/Webhook?type=1",
                (int)SupplierEnum.QiDtvm => $"{baseUrl}/Qi/Webhook?type=2",
                (int)SupplierEnum.QiConsultaPix => $"{baseUrl}/Qi/Webhook?type=3",
                (int)SupplierEnum.UnicoCheck => $"{baseUrl}/UnicoCheck/Update-Process",
                (int)SupplierEnum.UnicoId => $"{baseUrl}/UnicoCheck/Update-Id-Unico",
                (int)SupplierEnum.Arbi => $"{baseUrl}/Arbi/Web-Hook",
                (int)SupplierEnum.Bmp => $"{baseUrl}/Bmp/WebHook",
                (int)SupplierEnum.BrasilIndoc => $"{baseUrl}/Documentoscopia/WebHook",
                (int)SupplierEnum.Iugu => $"{baseUrl}/Iugu/WebHook-Iugu",
                (int)SupplierEnum.J17Operation => $"{baseUrl}/J17/Web-Hook",
                (int)SupplierEnum.J17Simulation => $"{baseUrl}/J17/Simulation-WebHook",
                (int)SupplierEnum.NycBank => $"{baseUrl}/NYC/WebHook",
                (int)SupplierEnum.OneBlink => $"{baseUrl}/OneBlink/Webhook-One-Blink",
                (int)SupplierEnum.NuVideo => $"{baseUrl}/Operation/Web-Hook-Nuvideo",
                (int)SupplierEnum.ParanaBank => $"{baseUrl}/ParanaBank/Webhook",
                _ => throw new Exception("Url não encontrada para o parâmetro")
            };
        }
    }
}
