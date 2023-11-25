using System;
using System.Diagnostics.Metrics;
using Npgsql;

using LibConfig;
using LibProjectMeta;
using LibCalculator;
using LibRayTracer;
using cs_xunit_dotnet.src.CalculatorCLI;


namespace cs_xunit_dotnet
{
    public class DatabasePostgres
    {
        private LibConfig.Config _fieldConfig;
        public DatabasePostgres ()
        {
            _fieldConfig = new LibConfig.Config();
            Console.WriteLine(_fieldConfig._fieldDatabaseConnectionString);
            using (NpgsqlConnection varConnection = new(_fieldConfig._fieldDatabaseConnectionString))
            {
                varConnection.Open();
                if (varConnection.State == System.Data.ConnectionState.Open)
                {
                    string varQuery = "SELECT version()";
                    using NpgsqlCommand varCMD = new(varQuery, varConnection);
                    string varVersion = varCMD.ExecuteScalar().ToString();
                    Console.WriteLine($"PostgreSQL Version: {varVersion}");
                }
                varConnection.Close();
            }
        }
        public void Create() {
            using (NpgsqlConnection varConnection = new(_fieldConfig._fieldDatabaseConnectionString))
            {
                varConnection.Open();
                if (varConnection.State == System.Data.ConnectionState.Open)
                {
                    string varQuery = "create table cars (id SERIAL PRIMARY KEY, name VARCHAR (255), price INT)";
                    using NpgsqlCommand varCMD = new();
                    varCMD.Connection = varConnection;
                    varCMD.CommandText = varQuery;
                    varCMD.ExecuteNonQuery();
                }
                varConnection.Close();
            }
        }
        public void Read() {
            using (NpgsqlConnection varConnection = new(_fieldConfig._fieldDatabaseConnectionString))
            {
                varConnection.Open();
                if (varConnection.State == System.Data.ConnectionState.Open)
                {
                    string varQuery = "INSERT INTO cars(name, price) VALUES(@name, @price)";
                    using NpgsqlCommand varCMD = new(varQuery, varConnection);
                    using NpgsqlDataReader varReader = varCMD.ExecuteReader();
                    while (varReader.Read())
                    {
                        Console.WriteLine($"{varReader.GetInt32(0)} - {varReader.GetString(1)}");
                    }
                }
                varConnection.Close();
            }
        }
        public void Update() {
            using (NpgsqlConnection varConnection = new(_fieldConfig._fieldDatabaseConnectionString))
            {
                varConnection.Open();
                if (varConnection.State == System.Data.ConnectionState.Open)
                {
                    string varQuery = "INSERT INTO cars(name, price) VALUES(@name, @price)";
                    using NpgsqlCommand varCMD = new(varQuery, varConnection);
                    varCMD.Parameters.AddWithValue("name", "BMW");
                    varCMD.Parameters.AddWithValue("price", "36600");
                    varCMD.Prepare();
                    varCMD.ExecuteNonQuery();
                }
                varConnection.Close();
            }
        }
        public void Delete() {}
    }
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
        static void rayTrace()
        {
            
            // int firepower = 10;
            // std::cout << "Firing: " << firepower << std::endl;
            // Simulation sim;
            // sim.fire(firepower);

            RayTracer varRT = new RayTracer();
            varRT.Test();
        }
        static void Main()
        {
            //DatabasePostgres varDB = new();
            rayTrace();
        }
    }
}