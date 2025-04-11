using Microsoft.Extensions.DependencyInjection;

namespace Documentator;

public static class Services
{
    public static IServiceCollection AddDocumentatorClients(this IServiceCollection services) => services
        .AddScoped<IHttpClient, HttpClient>()
        .AddTransient<IDocumentatorClient, DocumentatorClient>();
}
