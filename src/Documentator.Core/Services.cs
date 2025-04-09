using Microsoft.Extensions.DependencyInjection;

namespace Documentator.Core;

public static class Services
{
	public static IServiceCollection AddClients(this IServiceCollection services) => services
		.AddSingleton<HttpClientFactory>();
}