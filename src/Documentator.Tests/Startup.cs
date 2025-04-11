using Documentator.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Documentator.Tests;

[TestClass]
public class Startup
{
    public static ServiceProvider ServiceProvider;

    [AssemblyInitialize()]
    public static void AssemblyInit(TestContext context)
    {
        ServiceProvider = ConfigureServices().BuildServiceProvider();
    }

    [AssemblyCleanup()]
    public static async Task AssemblyCleanup()
    {
        await ServiceProvider.DisposeAsync();
    }

    public static IServiceCollection ConfigureServices()
    {
        DotEnv.Load();
        return new ServiceCollection()
            .AddClients()
            .AddDocumentatorClients();
    }
}
