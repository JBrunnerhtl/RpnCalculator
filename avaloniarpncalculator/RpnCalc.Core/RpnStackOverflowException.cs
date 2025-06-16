namespace RpnCalc.Core;

public class RpnStackOverflowException : Exception
{
    public RpnStackOverflowException(string message) : base(message)
    {
    }
}