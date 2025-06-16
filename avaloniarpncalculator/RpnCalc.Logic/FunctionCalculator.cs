namespace Logic;

public class FunctionCalculator
{
    public static double Calculate(double[] stack, double x)
    {
        stack = stack.Reverse().ToArray();
        double result = 0;
        int pow = 0;

        for (int i = 0; i < stack.Length; i++)
        {
            if (i == 0)
            {
                result += stack[i];
            }
            else
            {
                result += stack[i] * Math.Pow(x, pow); 
            }
            
            pow++;
        }
        return result;
    }
}