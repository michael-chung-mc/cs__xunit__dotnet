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
    protected List<LogData> history;
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
    public void Run()
    {
        ProjectMeta pm = new ProjectMeta();
        Calculator calculator = new Calculator();
        history = new List<LogData>();
        bool endApp = false;
        while (!endApp)
        {
            Console.WriteLine("Calculator says:\r\n");
            InputOperator();
            InputOperandX();
            InputOperandY();
            try
            {
                _fieldResult = calculator.Arithmetic(_fieldNumX, _fieldNumY, _fieldOperator);
                if (double.IsNaN(_fieldResult)) {
                    Console.WriteLine($"Error: {_fieldResult}. \n");
                }
                else Console.WriteLine($"{_fieldNumX} {_fieldOperator} {_fieldNumY} = " + String.Format("{0:0.##}\n", _fieldResult));
                LogData log = new LogData(_fieldNumX, _fieldNumY, _fieldOperator, _fieldResult);
                history.Add(log);
                pm.LogJson(log);
            }
            catch (Exception e) {
                Console.WriteLine("Objection!.\n - Details: " + e.Message);
            }
            foreach(LogData log in history)
            {
                Console.WriteLine(log.ToJson());
            }
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;
        }
        return;
    }
    public void InputOperandX ()
    {
        Console.Write("Operand? ");
        _fieldInput = Console.ReadLine();
        while (!double.TryParse(_fieldInput, out _fieldNumX))
        {
            Console.Write($"Invalid input. ${_fieldInput}. Operand? ");
            _fieldInput = Console.ReadLine();
        }
    }
    public void InputOperandY ()
    {
        Console.Write("Operand? ");
        _fieldInput = Console.ReadLine();
        while (!double.TryParse(_fieldInput, out _fieldNumY))
        {
            Console.Write($"Invalid input: ${_fieldInput}. Operand? ");
            _fieldInput = Console.ReadLine();
        }
    }
    public void InputOperator ()
    {
        Console.WriteLine("Supported Operations");
        Console.WriteLine("a - Add");
        Console.WriteLine("s - Subtract");
        Console.WriteLine("m - Multiply");
        Console.WriteLine("d - Divide");
        Console.WriteLine("q - Square Root");
        Console.WriteLine("p - Power");
        Console.WriteLine("e - Scientific Notation");
        Console.WriteLine("Operation?");
        _fieldOperator = Console.ReadLine();
    }
}
