namespace RpnCalc.Core;

public class RpnDivisionByZeroException : Exception
{
    public RpnDivisionByZeroException() : base("Division by zero is not allowed.") { }
    
    public RpnDivisionByZeroException(string message) : base(message) { }
}