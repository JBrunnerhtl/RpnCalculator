namespace RpnCalc.Core;

public class AddingEmptyException : Exception
{
    public AddingEmptyException(string message) : base(message) { }
}