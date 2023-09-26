using System.Numerics;

namespace WIPCalculator;
public class Calculator
{
    public Calculator()
    {
    }
    public double Arithmetic(double argX, double argY, string argOperation)
    {
        double result = double.NaN;
        switch (argOperation)
        {
            case "a":
                result = Add(argX,argY);
                break;
            case "s":
                result = Subtract(argX,argY);
                break;
            case "m":
                result = Multiply(argX,argY);
                break;
            case "d":
                Divide(argX,argY);
                break;
            default:
                break;
        }
        return result;
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
    public List<int> FibonacciIterative(int n)
    {
        return FibonacciIterative(n, Int32.MaxValue);
    }
    public List<int> FibonacciIterative(int n, int limit)
    {
        List<int> fib = new List<int>();
        fib.Add(1);
        if (n < 1)  { return fib; };
        fib.Add(1);
        if (n == 1) { return fib; };
        for (int i = 2; i < n; i++)
        {
            int nth = fib[i - 1] + fib[i - 2];
            if (nth > limit) { return fib; }
            fib.Add(nth);
        }
        return fib;
    }
    public BigInteger FibonacciSumEven(int n)
    {
        return FibonacciSumEven(n, Int32.MaxValue);
    }
    public BigInteger FibonacciSumEven(int n, int limit)
    {
        BigInteger sum = 0;
        List<int> f = FibonacciIterative(n, limit);
        for (int i = 0; i < f.Count(); i++)
        {
            BigInteger num = f[i];
            if (num % 2 == 0) { sum += num; }
        }
        return sum;
    }
}