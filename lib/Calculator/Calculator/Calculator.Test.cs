using Xunit;
using System.Numerics;
using LibCalculator;
public class FactorTest
{
    [Fact]
    public void Canary()
    {
        Assert.Equal(1, 1);
    }
    [Fact]
    public void Fibonacci_3__is_1_1_2()
    {
        Calculator f = new Calculator();
        List<int> fib = f.FibonacciIterative(3);
        List<int> res = new List<int>() { 1, 1, 2 };
        Assert.Equal(fib, res);
    }
    [Fact]
    public void FibonacciEvenSum_3__is_2()
    {
        Calculator f = new Calculator();
        BigInteger sum = f.FibonacciSumEven(3);
        BigInteger res = 2;
        Assert.Equal(sum, res);
    }
    [Fact]
    public void SumMultiple_0_0_0_Is0()
    {
        Calculator m = new Calculator();
        int res = m.SumMultiple(0, 0, 0);
        Assert.Equal(0, res);
    }
    [Fact]
    public void SumMultiple_3_5_10_Is23()
    {
        Calculator m = new Calculator();
        int res = m.SumMultiple(3, 5, 10);
        Assert.Equal(23, res);
    }
    [Fact]
    public void SumMultiple_N1_N1_0_IsN1()
    {
        Calculator m = new Calculator();
        int res = m.SumMultiple(-1, -1, 0);
        Assert.Equal(-1, res);
    }
    [Fact]
    public void MaxFactor_3__is_3()
    {
        Calculator f = new Calculator();
        BigInteger factor = f.MaxFactor(3);
        int max = 3;
        Assert.Equal(factor, max);
    }
}