using System;

namespace CalcTreeConsoleTest
{
    /// <summary>
    /// A MathNode which holds an operation as defined in Operation.cs.
    /// It can also evaluate the expression that the tree represents with the
    /// evaluate function.
    /// Both branches must be populated with an evaluatable node.
    /// </summary>
    public class OperatorNode : MathNode
    {
        /// <summary>
        /// The operation held by the node.
        /// </summary>
        public Operation op;
        
        /// <summary>
        /// The node on the left branch of the tree.
        /// </summary>
        public MathNode left;
        
        /// <summary>
        /// The node on the right branch of the tree.
        /// </summary>
        public MathNode right;
        
        /// <summary>
        /// Inaccessible. All currently implemented operations require two operands.
        /// </summary>
        private OperatorNode() {}
        
        /// <summary>
        /// Inaccessible. All currently implemented operations require two operands.
        /// </summary>
        private OperatorNode(Operation op) {}

        /// <summary>
        /// Inaccessible. All currently implemented operations require two operands.
        /// </summary>
        private OperatorNode(Operation op, MathNode left) {}

        /// <summary>
        /// Creates a new OperationNode, which represents a mathematical expression.
        /// The OperationNode contains the operation, and its branches are the operands.
        /// Each operand can be any node which inherits from MathNode and thus has an evaluate() function.
        /// </summary>
        /// <param name="op"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public OperatorNode(Operation op, MathNode left, MathNode right)
        {
            if (left == null)
            {
                throw new NullReferenceException("Left branch of OperatorNode cannot be null.");
            }
            
            if (right == null)
            {
                throw new NullReferenceException("Right branch of OperatorNode cannot be null.");
            }

            this.op = op;
            this.left = left;
            this.right = right;
        }
        /// <summary>
        /// Evaluates the expression represented by the expression tree rooted at the node this function is
        /// called on.
        /// </summary>
        /// <returns>A numeric node containing the result of the expression.</returns>
        /// <exception cref="NullReferenceException">If any branch of the expression tree is unexpectedly null</exception>
        /// <exception cref="InvalidOperationException">If a branch evaluation somehow returns null</exception>
        /// <exception cref="ArgumentException">If an operator node contains an invalid operation</exception>
        public MathNode evaluate()
        {
            if (left == null)
            {
                throw new NullReferenceException("Left branch of evaluated OperatorNode is null.");
            }

            if (right == null)
            {
                throw new NullReferenceException("Right branch of evaluated OperatorNode is null.");
            }

            NumericNode leftValue = this.left.evaluate() as NumericNode;
            NumericNode rightValue = this.right.evaluate() as NumericNode;

            if (leftValue == null)
            {
                throw new InvalidOperationException("Left branch could not be evaluated to a number.");
            }
                    
            if (rightValue == null)
            {
                throw new InvalidOperationException("Right branch could not be evaluated to a number.");
            }
            
            switch (op)
            {
                case Operation.Add:
                    return new NumericNode(leftValue.data + rightValue.data);
                
                case Operation.Subtract:
                    return new NumericNode(leftValue.data - rightValue.data);
                
                case Operation.Multiply:
                    return new NumericNode(leftValue.data * rightValue.data);
                
                case Operation.Divide:
                    return new NumericNode(leftValue.data / rightValue.data);
                
                case Operation.Exponentiate:
                    return new NumericNode(Math.Pow(leftValue.data, rightValue.data));
                
                default:
                    throw new ArgumentException("Invalid operation in OperatorNode operation field.");
            }
        }
        
        public override string ToString()
        {
            return this.op.ToString();
        }
        
    }
}