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
        Calculator varCalc = new Calculator();
        List<int> varFib = varCalc.FibonacciIterative(3);
        List<int> varRes = new List<int>() { 1, 1, 2 };
        Assert.Equal(varFib, varRes);
    }
    [Fact]
    public void FibonacciEvenSum_3__is_2()
    {
        Calculator varCalc = new Calculator();
        BigInteger varSum = varCalc.FibonacciSumEven(3);
        BigInteger varRes = 2;
        Assert.Equal(varSum, varRes);
    }
    [Fact]
    public void SumMultiple_0_0_0_Is0()
    {
        Calculator varCalc = new Calculator();
        int varRes = varCalc.SumMultiple(0, 0, 0);
        Assert.Equal(0, varRes);
    }
    [Fact]
    public void SumMultiple_3_5_10_Is23()
    {
        Calculator varCalc = new Calculator();
        int varRes = varCalc.SumMultiple(3, 5, 10);
        Assert.Equal(23, varRes);
    }
    [Fact]
    public void SumMultiple_N1_N1_0_IsN1()
    {
        Calculator varCalc = new Calculator();
        int varRes = varCalc.SumMultiple(-1, -1, 0);
        Assert.Equal(-1, varRes);
    }
    [Fact]
    public void MaxFactor_3__is_3()
    {
        Calculator varCalc = new Calculator();
        BigInteger varFactor = varCalc.MaxFactor(3);
        int varMax = 3;
        Assert.Equal(varFactor, varMax);
    }
}