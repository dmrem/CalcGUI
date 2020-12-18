using System.Globalization;

namespace CalcGUI
{

    /// <summary>
    /// A MathNode which only supports numeric data values and no branches.
    /// </summary>
    public class NumericNode : IMathNode
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

        /// <summary>
        /// Evaluates the expression tree rooted at the calling node.
        /// As a NumericNode never has any leaves, it just evaluates to itself.
        /// </summary>
        /// <returns>The NumericNode that this function was called on.</returns>
        public IMathNode Evaluate()
        {
            return this;
        }

        public override string ToString()
        {
            return data.ToString(CultureInfo.InvariantCulture);
        }
    }
}