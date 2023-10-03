using System;
using System.Diagnostics.Metrics;
using Npgsql;

using LibConfig;
using LibProjectMeta;
using LibCalculator;
using wip__cs__xunit.src.CalculatorCLI;

namespace wip__cs__xunit
{
    internal class Program
    {
        static void projectEuler()
        {
            Calculator varCalculator = new();
            Console.WriteLine("project euler problem 1 = sum of all the multiples of 3 or 5 below 1000");
            Console.WriteLine(varCalculator.SumMultiple(3, 5, 1000));
            Console.WriteLine("project euler problem 2 = sum of even of numbers < 4 million");
            Console.WriteLine(varCalculator.FibonacciSumEven(4000000));
            Console.WriteLine("project euler problem 3 = largest prime factor of 600851475143");
            Console.WriteLine(varCalculator.MaxFactor(600851475143));
        }
        static void Main()
        {
            LibConfig.Config varConfig = new LibConfig.Config();
            Console.WriteLine(varConfig._fieldDatabaseConnectionString);
            using (NpgsqlConnection varConnection = new(varConfig._fieldDatabaseConnectionString))
            {
                varConnection.Open();
                if (varConnection.State == System.Data.ConnectionState.Open)
                {
                    string varQuery = "select version()";
                    using NpgsqlCommand varCMD = new(varQuery, varConnection);
                    string varVersion = varCMD.ExecuteScalar().ToString();
                    Console.WriteLine($"PostgreSQL Version: {varVersion}");
                }
                varConnection.Close();
            }
        }
    }
}