using Logic;
using RpnCalc.Core;
using Xunit;

namespace Test;

public class StackCalculatorTests
{
    [Fact]
    public void Push_AddsValueToStack()
    {
        var calculator = new StackCalculator();
        calculator.Push(5);
        Assert.Single(calculator.Stack);
        Assert.Contains(5, calculator.Stack);
    }

    [Fact]
    public void Push_MultplyValuesToStack_ShouldReturn()
    {
        var stack = new StackCalculator();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);
        stack.Push(5);
        Assert.Throws<RpnStackOverflowException>(()=>stack.Push(6));
        Assert.Equal(5, stack.Stack.Count);
        
    }

    [Fact]
    public void Pop_RemovesAndReturnsTopValue()
    {
        var calculator = new StackCalculator();
        calculator.Push(5);
        calculator.Push(10);
        var result = calculator.Pop();
        Assert.Equal(10, result);
        Assert.Single(calculator.Stack);
    }

    [Fact]
    public void Pop_ThrowsException_WhenStackIsEmpty()
    {
        var calculator = new StackCalculator();
        Assert.Throws<RpnStackUnderflowException>(() => calculator.Pop());
    }

    [Fact]
    public void Add_AddsTopTwoValues()
    {
        var calculator = new StackCalculator();
        calculator.Push(3);
        calculator.Push(7);
        calculator.Add();
        Assert.Single(calculator.Stack);
        Assert.Equal(10, calculator.Stack.First());
    }

    [Fact]
    public void Add_ThrowsException_WhenNotEnoughValues()
    {
        var calculator = new StackCalculator();
        calculator.Push(3);
        Assert.Throws<RpnStackUnderflowException>(() => calculator.Add());
    }

    [Fact]
    public void Subtract_SubtractsTopTwoValues()
    {
        var calculator = new StackCalculator();
        calculator.Push(10);
        calculator.Push(3);
        calculator.Subtract();
        Assert.Single(calculator.Stack);
        Assert.Equal(7, calculator.Stack.First());
    }

    [Fact]
    public void Subtract_ThrowsException_WhenNotEnoughValues()
    {
        var calculator = new StackCalculator();
        calculator.Push(3);
        Assert.Throws<RpnStackUnderflowException>(() => calculator.Subtract());
    }

    [Fact]
    public void Multiply_MultipliesTopTwoValues()
    {
        var calculator = new StackCalculator();
        calculator.Push(4);
        calculator.Push(5);
        calculator.Multiply();
        Assert.Single(calculator.Stack);
        Assert.Equal(20, calculator.Stack.First());
    }

    [Fact]
    public void Multiply_ThrowsException_WhenNotEnoughValues()
    {
        var calculator = new StackCalculator();
        calculator.Push(3);
        Assert.Throws<RpnStackUnderflowException>(() => calculator.Multiply());
    }

    [Fact]
    public void Divide_DividesTopTwoValues()
    {
        var calculator = new StackCalculator();
        calculator.Push(10);
        calculator.Push(2);
        calculator.Divide();
        Assert.Single(calculator.Stack);
        Assert.Equal(5, calculator.Stack.First());
    }

    [Fact]
    public void Divide_ThrowsException_WhenDividingByZero()
    {
        var calculator = new StackCalculator();
        calculator.Push(10);
        calculator.Push(0);
        Assert.Throws<RpnDivisionByZeroException>(() => calculator.Divide());
    }

    [Fact]
    public void Divide_ThrowsException_WhenNotEnoughValues()
    {
        var calculator = new StackCalculator();
        calculator.Push(3);
        Assert.Throws<RpnStackUnderflowException>(() => calculator.Divide());
    }

    [Fact]
    public void Swap_SwapsTopTwoValues()
    {
        var calculator = new StackCalculator();
        calculator.Push(1);
        calculator.Push(2);
        calculator.Swap();
        Assert.Equal(2, calculator.Stack.ElementAt(1));
        Assert.Equal(1, calculator.Stack.First());
    }

    [Fact]
    public void Swap_ThrowsException_WhenNotEnoughValues()
    {
        var calculator = new StackCalculator();
        calculator.Push(3);
        Assert.Throws<RpnStackUnderflowException>(() => calculator.Swap());
    }

    [Fact]
    public void Clear_RemovesAllValuesFromStack()
    {
        var calculator = new StackCalculator();
        calculator.Push(1);
        calculator.Push(2);
        calculator.Clear();
        Assert.Empty(calculator.Stack);
    }

    [Fact]
    public void GetStackSnapshot_ReturnsStackAsArray()
    {
        var calculator = new StackCalculator();
        calculator.Push(1);
        calculator.Push(2);
        var snapshot = calculator.GetStackSnapshot();
        Assert.Equal(new[] { 2.0, 1.0 }, snapshot);
    }
}