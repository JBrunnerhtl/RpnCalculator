namespace RpnCalc.Core;

public class RpnStackUnderflowException : Exception
{
    public RpnStackUnderflowException(string message) : base(message)
    {
    }
    
}