namespace LibConfig;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
public class Config
{
    public void Startup()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            //.AddJsonFile("appsettings.json")
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
            .AddEnvironmentVariables()
            .Build();
        var settings = config.GetConnectionString("Test");
        Console.WriteLine(settings);
    }
}
