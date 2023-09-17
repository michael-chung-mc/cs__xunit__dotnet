using System;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace wip__cs__xunit.CalculatorLibrary
{
    public class CalculatorAttribute
    {
        public double OperandX { get; set; }
        public double OperandY { get; set; }
        public string Operation { get; set; }
        public double Result { get; set; }
    }
    public class CalculatorLibrary
    {
        private void Log(double numx, double numy, string operation, double result)
        {
            Trace.WriteLine(String.Format("{0} {1} {2} = {3}", numx, operation, numy, result));
            string fileName = "__calculator.json";
            CalculatorAttribute ca = new CalculatorAttribute();
            ca.OperandX = numx;
            ca.OperandY = numy;
            ca.Operation = operation;
            ca.Result = result;
            string json = JsonSerializer.Serialize(ca);
            File.WriteAllText(fileName, json);
        }
        public CalculatorLibrary()
        {
            if (!File.Exists("__calculator.log"))
            {
                File.Create("__calculator.log").Dispose();
            }
            using (StreamWriter logFile = new StreamWriter("__calculator.log", false))
            {
                logFile.AutoFlush = true;
                Trace.Listeners.Add(new TextWriterTraceListener(logFile));
                Trace.AutoFlush = true;
                Trace.WriteLine("Starting Calculator Log");
                Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));
            }
        }
        public double Calc(double numx, double numy, string operation)
        {
            double result = double.NaN;
            switch (operation)
            {
                case "a":
                    result = numx + numy;
                    Log(numx, numy, operation, result);
                    break;
                case "s":
                    result = numx - numy;
                    Log(numx, numy, operation, result);
                    break;
                case "m":
                    result = numx * numy;
                    Log(numx, numy, operation, result);
                    break;
                case "d":
                    if (numy != 0)
                    {
                        result = numx / numy;
                        Log(numx, numy, operation, result);
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}