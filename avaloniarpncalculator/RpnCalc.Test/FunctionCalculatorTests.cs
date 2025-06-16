using Logic;

namespace Test;

public class FunctionCalculatorTests
{
    [Fact]
    public void Calculate_ShouldReturnCorrectValue_ForPolynomial()
    {
        // Arrange
        double[] stack = { 1, 2, 3 }; // Represents 1 + 2x + 3x^2
        double x = 2;
        
        // Act
        double result = FunctionCalculator.Calculate(stack, x);

        // Assert
        Assert.Equal(3 + 2 * x + 1 * x * x, result);
    }
}