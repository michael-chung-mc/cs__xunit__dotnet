namespace LibCalculator;
using System.Numerics;
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
    // sum of all multiples x or y below z
    public int SumMultiple(int x, int y, int z)  {
        if (x < 0 || y < 0 || z < 0) { return -1; }
        int sum = 0;
        for (int i = 0; i < z; i++) {
            if (i%x == 0 || i%y == 0)  { sum += i; }    
        }
        return sum;
    }
    public BigInteger MaxFactor(BigInteger n)
    {
        if (n < 4) { return n; }
        BigInteger maxFactor = 0;
        BigInteger factor = 2;
        while (n > 1)
        {
            if (n % factor == 0)
            {
                maxFactor = factor;
                n /= factor;
            }
            else
            {
                factor += 1;
            }
        }
        return maxFactor;
    }
}