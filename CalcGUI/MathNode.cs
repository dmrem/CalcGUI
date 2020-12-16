using System;
using System.Collections.Generic;
using System.Text;

namespace CalcTreeConsoleTest
{
    public interface MathNode
    {
        /// <summary>
        /// Finds the value represented by this tree. If the node that this function is called on is an operation, 
        /// performs that operation using its left and right branches as its operands. If the node is a number, just
        /// returns that number.
        /// </summary>
        /// <returns>The result of the calculation.</returns>
        MathNode evaluate();
    }
}
