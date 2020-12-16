using System;
using System.Globalization;

namespace CalcTreeConsoleTest
{
    
    /// <summary>
    /// A MathNode which only supports numeric data values and no branches.
    /// </summary>
    public class NumericNode : MathNode
    {
        
        /// <summary>
        /// The data held by the node.
        /// </summary>
        public double data;

        /// <summary>
        /// Creates a new NumericNode. This node represents a number to be used in an expression.
        /// This node can have no branches.
        /// </summary>
        /// <param name="data"></param>
        public NumericNode(double data)
        {
            this.data = data;
        }

        public MathNode evaluate()
        {
            return this;
        }

        public override string ToString()
        {
            return this.data.ToString(CultureInfo.InvariantCulture);
        }
    }
}