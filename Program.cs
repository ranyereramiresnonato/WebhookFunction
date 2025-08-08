using ForwardWebhook.Services.HttpClientService;
using ForwardWebhook.Services.UrlService;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddHttpClient<HttpClientService>();
        services.AddScoped<IHttpClientService, HttpClientService>();
        services.AddScoped<IUrlService, UrlService>();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
