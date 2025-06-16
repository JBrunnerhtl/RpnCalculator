using System.Collections.Generic;


namespace RpnCalc.Core
{
    /// <summary>
    /// Defines the interface for a basic Reverse Polish Notation calculator.
    /// </summary>
    public interface IRpnCalculator
    {
        /// <summary>
        /// Provides read-only access to the current stack.
        /// </summary>
        IReadOnlyCollection<double> Stack { get; }

        /// <summary>
        /// Pushes a number onto the stack.
        /// </summary>
        void Push(double value);

        /// <summary>
        /// Pops and returns the top value of the stack.
        /// Throws RpnStackUnderflowException if the stack is empty.
        /// </summary>
        /// <exception cref="RpnStackUnderflowException"/>
        double Pop();

        /// <summary>
        /// Performs addition with the top two elements on the stack.
        /// Throws RpnStackUnderflowException if fewer than two elements exist.
        /// </summary>
        /// <exception cref="RpnStackUnderflowException"/>
        void Add();

        /// <summary>
        /// Performs subtraction with the top two elements on the stack (second - top).
        /// Throws RpnStackUnderflowException if fewer than two elements exist.
        /// </summary>
        /// <exception cref="RpnStackUnderflowException"/>
        void Subtract();

        /// <summary>
        /// Performs multiplication with the top two elements on the stack.
        /// Throws RpnStackUnderflowException if fewer than two elements exist.
        /// </summary>
        /// <exception cref="RpnStackUnderflowException"/>
        void Multiply();

        /// <summary>
        /// Performs division with the top two elements on the stack (second / top).
        /// Throws RpnStackUnderflowException if fewer than two elements exist.
        /// Throws RpnDivisionByZeroException if division by zero occurs.
        /// </summary>
        /// <exception cref="RpnStackUnderflowException"/>
        /// <exception cref="RpnDivisionByZeroException"/>
        void Divide();

        /// <summary>
        /// Swaps the top two elements on the stack.
        /// Throws RpnStackUnderflowException if fewer than two elements exist.
        /// </summary>
        /// <exception cref="RpnStackUnderflowException"/>
        void Swap();

        /// <summary>
        /// Clears all elements from the stack.
        /// </summary>
        void Clear();

        /// <summary>
        /// Returns a snapshot of the current stack as an array (top of stack is last).
        /// </summary>
        double[] GetStackSnapshot();
    }
}