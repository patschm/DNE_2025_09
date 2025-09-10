using Microsoft.Extensions.DependencyInjection;
using System.Security.Authentication.ExtendedProtection;

namespace Dependencies;

internal class Program
{
    static void Main(string[] args)
    {
        var factory = new DefaultServiceProviderFactory();
        ServiceCollection services = new ServiceCollection();
        services.AddTransient<CounterContainer, CounterContainer>();
        services.AddSingleton<ICounter, AntiCounter>();
        var provider =  factory.CreateServiceProvider(services);

        //var container = new CounterContainer(new Counter());

        var sc1 = provider.CreateScope();
        var container = sc1.ServiceProvider.GetRequiredService<CounterContainer>();
        container.DoeJeDing();

        container = sc1.ServiceProvider.GetRequiredService<CounterContainer>();
        container.DoeJeDing();
        sc1.Dispose();

        var sc2 = provider.CreateScope();
        container = sc2.ServiceProvider.GetRequiredService<CounterContainer>();
        container.DoeJeDing();

        container = sc2.ServiceProvider.GetRequiredService<CounterContainer>();
        container.DoeJeDing();

        sc2.Dispose();

        var sc3 = provider.CreateScope();
        container = sc3.ServiceProvider.GetRequiredService<CounterContainer>();
        container.DoeJeDing();

        container = sc3.ServiceProvider.GetRequiredService<CounterContainer>();
        container.DoeJeDing();
        sc3.Dispose();
    }
}
