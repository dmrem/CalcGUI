using System;
using System.Collections;

namespace CalcGUI
{
    public static class CalcTree
    {
        /// <summary>
        /// The operator precedence definitions. The higher the number, the earlier an operation is to be performed, ignoring parentheses.
        /// The ( operator is there because the algorithm requires it, not because ( is a legitimate operator
        /// </summary>
        public static readonly Hashtable precedence = new Hashtable
        {
            {"^", 6},
            {"/", 5},
            {"*", 5},
            {"+", 4},
            {"-", 4},
            {"(", 0}
        };

        /// <summary>
        /// Converts a given infix expression to a binary expression tree. Each token in the expression string is delimited by spaces.
        /// Valid tokens are any number representable as a double, any operator in the precedence table above, and parentheses. 
        /// </summary>
        /// <param name="s">The expression to convert.</param>
        /// <returns></returns>
        public static IMathNode ConvertStringToTree(String s)
        {
            // step one is to convert the given string to reverse polish notation
            // will use Dijkstra's shunting yard algorithm
            // used tutorial of algorithm from http://www.learn4master.com/algorithms/convert-infix-notation-to-reverse-polish-notation-java
            // used pseudocode but not java example

            String[] tokens = s.Split(' ');

            Queue RPNQueue = new Queue();
            Stack OperatorStack = new Stack();

            foreach (string token in tokens)
            {
                // if the token is a number, put it into the queue as it is
                double currentNumber;
                if (Double.TryParse(token, out currentNumber))
                {
                    RPNQueue.Enqueue(currentNumber);
                    continue;
                }

                // if it's an open paren, push it onto the stack to be retrieved later
                if (token == "(")
                {
                    OperatorStack.Push(token);
                    continue;
                }

                // if it's a close paren, push everything on the stack up to the open paren into the output queue
                if (token == ")")
                {
                    while (OperatorStack.Peek() as String != "(")
                    {
                        RPNQueue.Enqueue(OperatorStack.Pop());
                    }
                    OperatorStack.Pop(); // remove the "("
                    continue;
                }

                // if the token isn't a number or paren, it must be an operator
                // if the stack contains an operator with higher precedence than the current one,
                // put the stack operator in the queue
                // repeat until stack is empty or has an op of lower precedence than the current one
                // then push current op onto stack
                while (OperatorStack.Count != 0 &&
                       (int)precedence[OperatorStack.Peek() as String] >= (int)precedence[token])
                {
                    RPNQueue.Enqueue(OperatorStack.Pop());
                }
                OperatorStack.Push(token);

            }

            while (OperatorStack.Count != 0)
            {
                RPNQueue.Enqueue(OperatorStack.Pop());
            }

            // step 2 is to convert the rpn into a tree
            // easiest way to do that that I can think of without reading more tutorials
            // is to convert from postfix to prefix
            // to do that, just reverse the queue

            while (RPNQueue.Count != 0)
            {
                OperatorStack.Push(RPNQueue.Dequeue());
            }

            while (OperatorStack.Count != 0)
            {
                RPNQueue.Enqueue(OperatorStack.Pop());
            }

            // helper function to recursively generate a tree
            IMathNode generateTree(Queue RPN)
            {
                if (RPN.Peek() is double)
                {
                    return new NumericNode((double)RPN.Dequeue());
                }

                IMathNode left, right;
                switch ((string)RPN.Dequeue())
                {
                    case "+":
                        right = generateTree(RPN);
                        left = generateTree(RPN);
                        return new OperatorNode(Operation.Add, left, right);
                    case "-":
                        right = generateTree(RPN);
                        left = generateTree(RPN);
                        return new OperatorNode(Operation.Subtract, left, right);
                    case "*":
                        right = generateTree(RPN);
                        left = generateTree(RPN);
                        return new OperatorNode(Operation.Multiply, left, right);
                    case "/":
                        right = generateTree(RPN);
                        left = generateTree(RPN);
                        return new OperatorNode(Operation.Divide, left, right);
                    case "^":
                        right = generateTree(RPN);
                        left = generateTree(RPN);
                        return new OperatorNode(Operation.Exponentiate, left, right);
                    default:
                        throw new InvalidOperationException("Invalid operator in expression.");
                }
            }

            return generateTree(RPNQueue);
        }
    }
}