using System.Diagnostics;
using LibCalculator;
using LibProjectMeta;

namespace wip__cs__xunit.src.CalculatorCLI;

public class CalculatorCLI
{
    protected string _fieldInput = "";
    protected double _fieldNumX = 0;
    protected double _fieldNumY = 0;
    protected double _fieldResult = 0;
    protected string _fieldOperator = "";
    public void loop()
    {
        ProjectMeta pm = new ProjectMeta();
        Calculator calculator = new Calculator();
        bool endApp = false;
        while (!endApp)
        {
            Console.WriteLine("Calculator says:\r\n");
            // operand one
            Console.Write("X? ");
            _fieldInput = Console.ReadLine();
            while (!double.TryParse(_fieldInput, out _fieldNumX))
            {
                Console.Write("Invalid input. X? ");
                _fieldInput = Console.ReadLine();
            }
            // operand two
            Console.Write("Y? ");
            _fieldInput = Console.ReadLine();
            while (!double.TryParse(_fieldInput, out _fieldNumY))
            {
                Console.Write("Invalid input. Y? ");
                _fieldInput = Console.ReadLine();
            }

            // operator.
            Console.WriteLine("Supported Operations");
            Console.WriteLine("a - Add");
            Console.WriteLine("s - Subtract");
            Console.WriteLine("m - Multiply");
            Console.WriteLine("d - Divide");
            Console.WriteLine("Operation?");
            _fieldOperator = Console.ReadLine();

            try
            {
                _fieldResult = calculator.Arithmetic(_fieldNumX, _fieldNumY, _fieldOperator);
                LogData log = new LogData(_fieldNumX, _fieldNumY, _fieldOperator, _fieldResult);
                pm.LogJson(log);
                if (double.IsNaN(_fieldResult))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine("Your result: {0:0.##}\n", _fieldResult);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        return;
    }
    protected struct LogData : LibProjectMeta.LogData
    {
        double _fieldX = 0;
        double _fieldY = 0;
        string _fieldOp = "";
        double _fieldAns = 0;
        public LogData(double argX, double argY, string argOperation, double argResult)
        {
            _fieldX = argX;
            _fieldY = argY;
            _fieldOp = argOperation;
            _fieldAns = argResult;
        }
        public string ToJson ()
        {
            return $"{{\"Time\":\"{System.DateTime.Now}\", \"Operand\":{_fieldX}, \"OperandY\":{_fieldY}, \"Operation\":\"{_fieldOp}\", \"Result\":{_fieldAns}}}";
        }
    }
}
