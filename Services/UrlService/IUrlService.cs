using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForwardWebhook.Services.UrlService
{
    public interface IUrlService
    {
        string SearchUrlForSending(int supplierIdentifier);
    }
}
