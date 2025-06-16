using RpnCalc.Core;
using RpnCalc;
namespace Logic;

public class StackCalculator : IRpnCalculator
{
    private Stack<double> _stack;

    public StackCalculator()
    {
        _stack = new Stack<double>();
    }


    public IReadOnlyCollection<double> Stack => _stack;
    public void Push(double value)
    {
        if (_stack.Count >= 5) throw new RpnStackOverflowException("Der Stack ist voll.\n Es können keine weiteren Werte hinzugefügt werden.");
        _stack.Push(value);
    }

    public double Pop()
    {
        if (_stack.Count == 0)
        {
            throw new RpnStackUnderflowException("Es wurde nichts eingefügt. Bitte füge zuerst eine Zahl ein.");
        }
        return _stack.Pop();
    }

    public void Add()
    {
        if (_stack.Count < 2)
        {
            throw new RpnStackUnderflowException("Es sind nicht genügend Elemente vorhanden,\n um eine Addition durchzuführen.");
        }
        
        double second = _stack.Pop();
        double first = _stack.Pop();
        double result = first + second;
        _stack.Push(result);
    }

    public void Subtract()
    {
        if (_stack.Count < 2)
        {
            throw new RpnStackUnderflowException("Es sind nicht genügend Elemente vorhanden,\n um eine Subtraktion durchzuführen.");
        }
        
        double second = _stack.Pop();
        double first = _stack.Pop();
        double result = first - second;
        _stack.Push(result);
    }

    public void Multiply()
    {
        if (_stack.Count < 2)
        {
            throw new RpnStackUnderflowException("Es sind nicht genügend Elemente vorhanden,\n um eine Multiplikation durchzuführen.");
        }
        
        double second = _stack.Pop();
        double first = _stack.Pop();
        double result = first * second;
        _stack.Push(result);
    }

    public void Divide()
    {
        if (_stack.Count < 2)
        {
            throw new RpnStackUnderflowException("Es sind nicht genügend Elemente vorhanden,\n um eine Division durchzuführen.");
        }
        
        double second = _stack.Pop();
        if (second == 0)
        {
            _stack.Push(second);
            throw new RpnDivisionByZeroException("Man kann nicht durch Null dividieren.");
        }
        
        double first = _stack.Pop();
        double result = first / second;
        _stack.Push(result);
    }

    public void Swap()
    {
        if (_stack.Count < 2)
        {
            throw new RpnStackUnderflowException("Es sind nicht genügend Elemente vorhanden,\n um die Werte zu tauschen.");
        }
        
        double first = _stack.Pop();
        double second = _stack.Pop();
        _stack.Push(first);
        _stack.Push(second);
    }

    public void Clear()
    {
        _stack = new Stack<double>();
    }

    public double[] GetStackSnapshot()
    {
        return _stack.ToArray();
    }
}