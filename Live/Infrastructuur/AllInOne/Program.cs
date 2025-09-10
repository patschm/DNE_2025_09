using Dependencies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.Metrics;

namespace AllInOne;

internal class Program
{
    static void Main(string[] args)
    {
        var builder = new HostApplicationBuilder();

        //builder.Configuration["pass"];
        builder.Services.AddTransient<CounterContainer>();
        builder.Services.AddTransient<ICounter, AntiCounter>();


        IHost host =  builder.Build();

        var cnt = host.Services.GetRequiredService<CounterContainer>();

        Console.WriteLine(builder.Configuration["Password"]);

        cnt.DoeJeDing();

    }
}
