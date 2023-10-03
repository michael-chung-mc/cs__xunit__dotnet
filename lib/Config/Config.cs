namespace LibConfig;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
public class Config
{
    public string _fieldDatabaseConnectionString;
    public Config()
    {
        IConfigurationRoot varConfig = new ConfigurationBuilder()
            //.AddJsonFile("appsettings.json")
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
            .AddEnvironmentVariables()
            .Build();
        _fieldDatabaseConnectionString = varConfig.GetConnectionString("Test");
    }
}
