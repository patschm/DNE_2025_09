using Microsoft.Extensions.Configuration;

namespace Configuratie;

internal class Program
{
    static void Main(string[] args)
    {
        string constr = Environment.GetEnvironmentVariable("Pass");
        Console.WriteLine(constr);
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);

        //Console.WriteLine(config["Pass"]);
        builder.AddJsonFile("appsettings.json", optional:true, reloadOnChange:true);
        builder.AddUserSecrets("bob");
        builder.AddEnvironmentVariables();
        //builder.AddAzureAppConfiguration(constr);

        IConfiguration config = builder.Build();

        string first = config.GetSection("Person:Firstname").Value;
        Console.WriteLine(first);

        //Console.ReadLine();
        //first = config.GetSection("Person:Firstname").Value;
        //Console.WriteLine(first);

        Console.WriteLine(config["PASSWORD"]);
        Console.WriteLine(config["Pass"]);

        Person p = config.GetSection("Person").Get<Person>();

        Console.WriteLine($"{p.Firstname} {p.Lastname}");

        Person p2 = new();

        config.GetSection("Person").Bind(p2);

    }
}
