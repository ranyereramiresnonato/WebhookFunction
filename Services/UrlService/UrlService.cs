using ForwardWebhook.Enums;

namespace ForwardWebhook.Services.UrlService
{
    public class UrlService : IUrlService
    {
        private UrlService()
        {

        }
        public string SearchUrlForSending(int supplierIdentifier)
        {
            string BaseUrl = "https://localhost:5001/Api/V1";

            switch (supplierIdentifier)
            {
                case (int)SupplierEnum.QiSociedadeDeCredito:
                    return BaseUrl += "/Qi/WebHook?type=0";
                case (int)SupplierEnum.QiInss:
                    return BaseUrl += "/Qi/Webhook?type=1";
                case(int)SupplierEnum.QiDtvm:
                    return BaseUrl += "/Qi/Webhook?type=2";
                case (int)SupplierEnum.QiConsultaPix:
                    return BaseUrl += "/Qi/Webhook?type=3";
                case (int)SupplierEnum.UnicoCheck:
                    return BaseUrl += "/UnicoCheck/Update-Process";
                case (int)SupplierEnum.UnicoId:
                    return BaseUrl += "/UnicoCheck/Update-Id-Unico";
                case (int)SupplierEnum.Arbi:
                    return BaseUrl += "/Arbi/Web-Hook";
                case (int)SupplierEnum.Bmp:
                    return BaseUrl += "/Bmp/WebHook";
                case (int)SupplierEnum.BrasilIndoc:
                    return BaseUrl += "/Documentoscopia/WebHook";
                case (int)SupplierEnum.Iugu:
                    return BaseUrl += "/Iugu/WebHook-Iugu";
                case (int)SupplierEnum.J17Operation:
                    return BaseUrl += "/J17/Web-Hook";
                case (int)SupplierEnum.J17Simulation:
                    return BaseUrl += "/J17/Simulation-WebHook";
                case (int)SupplierEnum.NycBank:
                    return BaseUrl += "/NYC/WebHook";
                case (int)SupplierEnum.OneBlink:
                    return BaseUrl += "/OneBlink/Webhook-One-Blink";
                case (int)SupplierEnum.NuVideo:
                    return BaseUrl += "/Operation/Web-Hook-Nuvideo";
                case (int)SupplierEnum.ParanaBank:
                    return BaseUrl += "/ParanaBank/Webhook";
                default: throw new Exception("Url não encontrada para o parâmetro");
            }
        }
    }
}
