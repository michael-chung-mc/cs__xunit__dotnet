using System;
using System.Diagnostics.Metrics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Npgsql;

using LibProjectMeta;
using LibCalculator;
using wip__cs__xunit.src.CalculatorCLI;
using Npgsql.Replication;
using System.Reflection;

namespace wip__cs__xunit
{
    internal class Program
    {
        static void projectEuler()
        {
            Calculator cr = new();
            Console.WriteLine("project euler problem 1 = sum of all the multiples of 3 or 5 below 1000");
            Console.WriteLine(cr.SumMultiple(3, 5, 1000));
            Console.WriteLine("project euler problem 2 = sum of even of numbers < 4 million");
            Console.WriteLine(cr.FibonacciSumEven(4000000));
            Console.WriteLine("project euler problem 3 = largest prime factor of 600851475143");
            Console.WriteLine(cr.MaxFactor(600851475143));
        }
        static void Main()
        {
            // var configuration = new ConfigurationBuilder()
            //     .AddInMemoryCollection(new Dictionary<string, string?> ()
            //     {
            //         ["Key"] = "Value"
            //     }).Build();
            // Console.WriteLine(configuration["Key"]);
            // CalculatorCLI c = new CalculatorCLI();
            // c.Run();
            // IConfigurationBuilder builder = new IConfigurationBuilder();
            // IConfiguration config = builder.Build();
            // IConfiguration config = this.Configuration;
            //var config = new ConfigurationBuilder();
            //var connectionString = config.GetSection("ConnectionStrings").Bind();
            //string connectionString = config.GetConnectionString("TestDB");
            // config.Build();
            //var config = new IConfiguration();
            //var connectionString = Configuration.GetSection("TestDB").Get<>();
            //Console.WriteLine("HelloWorld");
            //Console.WriteLine(connectionString.ToString());
            IConfigurationRoot config = new ConfigurationBuilder()
                //.AddJsonFile("appsettings.json")
                .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
                .AddEnvironmentVariables()
                .Build();
            var settings = config.GetConnectionString("Test");
            Console.WriteLine(settings);
        }
    }
}