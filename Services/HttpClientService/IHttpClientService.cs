using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForwardWebhook.Services.HttpClientService
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> PostAsync(string url, object body);
    }
}
