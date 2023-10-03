namespace LibCalculator;
using System.Numerics;
public class Calculator
{
    public Calculator()
    {
    }
    public double Arithmetic(double argX, double argY, string argOperation)
    {
        double varResult = double.NaN;
        switch (argOperation)
        {
            case "a":
                varResult = Add(argX,argY);
                break;
            case "s":
                varResult = Subtract(argX,argY);
                break;
            case "m":
                varResult = Multiply(argX,argY);
                break;
            case "d":
                varResult = Divide(argX,argY);
                break;
            case "q":
                varResult = SquareRoot(argX);
                break;
            case "p":
                varResult = Power(argX,argY);
                break;
            case "e":
                varResult = ScientificNotation(argX,argY);
                break;
            default:
                break;
        }
        return varResult;
    }
    public double Add (double argX, double argY)
    {
        return argX + argY;
    }
    public double Subtract (double argX, double argY)
    {
        return argX - argY;
    }
    public double Multiply (double argX, double argY)
    {
        return argX * argY;
    }
    public double Divide (double argX, double argY)
    {
        return argY == 0 ? double.NaN : argX / argY;
    }
    public double SquareRoot(double argX)
    {
        return Math.Sqrt(argX);
    }
    public double Power (double argX, double argY)
    {
        return Math.Pow(argX,argY);
    }
    public double ScientificNotation (double argX, double argY)
    {
        return argX * Math.Pow(10,(int)argY);
    }
    public List<int> FibonacciIterative(int n)
    {
        return FibonacciIterative(n, Int32.MaxValue);
    }
    public List<int> FibonacciIterative(int argN, int argLimit)
    {
        List<int> varListFib = new List<int>();
        varListFib.Add(1);
        if (argN < 1)  { return varListFib; };
        varListFib.Add(1);
        if (argN == 1) { return varListFib; };
        for (int i = 2; i < argN; i++)
        {
            int varNth = varListFib[i - 1] + varListFib[i - 2];
            if (varNth > argLimit) { return varListFib; }
            varListFib.Add(varNth);
        }
        return varListFib;
    }
    public BigInteger FibonacciSumEven(int argN)
    {
        return FibonacciSumEven(argN, Int32.MaxValue);
    }
    public BigInteger FibonacciSumEven(int argN, int argLimit)
    {
        BigInteger varSum = 0;
        List<int> varFib = FibonacciIterative(argN, argLimit);
        for (int i = 0; i < varFib.Count(); i++)
        {
            BigInteger varNum = varFib[i];
            if (varNum % 2 == 0) { varSum += varNum; }
        }
        return varSum;
    }
    // sum of all multiples x or y below z
    public int SumMultiple(int argX, int argY, int argZ)  {
        if (argX < 0 || argY < 0 || argZ < 0) { return -1; }
        int varSum = 0;
        for (int i = 0; i < argZ; i++) {
            if (i%argX == 0 || i%argY == 0)  { varSum += i; }    
        }
        return varSum;
    }
    public BigInteger MaxFactor(BigInteger argN)
    {
        if (argN < 4) { return argN; }
        BigInteger varMaxFactor = 0;
        BigInteger varFactor = 2;
        while (argN > 1)
        {
            if (argN % varFactor == 0)
            {
                varMaxFactor = varFactor;
                argN /= varFactor;
            }
            else
            {
                varFactor += 1;
            }
        }
        return varMaxFactor;
    }
}