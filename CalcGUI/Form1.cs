using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalcGUI
{
    public partial class Form1 : Form
    {
        String lastButtonText = "";
        String currentExpr = "";
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles all button presses. Currently handles three different classes of buttons: numbers, operators, and parentheses.
        /// Behaviour differs depending on if the button that is pressed is of the same class as the previous button pressed.
        /// The "." button and "+/-" button technically fit in the number class, as they modify numbers and start new ones, and the
        /// "=" button is in a class of its own because it starts the evaluation of the expression. It is a sort of meta-operator, if you will.
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The event arguments. Unused.</param>
        private void btn_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if(sender == null)
            {
                throw new NullReferenceException("Button handler called with non-button sender.");
            }

            if (isNumber(b.Text))
            {
                if (isNumber(lastButtonText))
                {
                    lastButtonText += b.Text;
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
                else if (lastButtonText == "(" || lastButtonText == ")")
                {
                    currentExpr += " " + lastButtonText;
                    lastButtonText = b.Text;
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
                else
                {
                    currentExpr += " " + lastButtonText;
                    lastButtonText = b.Text;
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
            }
            else if(b.Text == "(" || b.Text == ")")
            {
                if (currentExpr == "")
                {
                    lastButtonText = b.Text;
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
                else
                {
                    currentExpr += " " + lastButtonText;
                    lastButtonText = b.Text;
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
            }
            else if(b.Text == "+" || b.Text == "-" || b.Text == "*" || b.Text == "/" || b.Text == "^")
            {
                if(currentExpr == "")
                {
                    lastButtonText = b.Text;
                    currentExpr = "0";
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
                else if (lastButtonText == "(" || lastButtonText == ")")
                {
                    currentExpr += " " + lastButtonText;
                    lastButtonText = b.Text;
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
                else if (isNumber(lastButtonText))
                {
                    currentExpr += " " + lastButtonText;
                    lastButtonText = b.Text;
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
                else
                {
                    lastButtonText = b.Text;
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
            }
            else if(b.Text == ".")
            {
                if (isNumber(lastButtonText))
                {
                    lastButtonText += b.Text;
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
                else
                {
                    currentExpr += " " + lastButtonText;
                    lastButtonText = "0" + b.Text;
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
            }
            else if(b.Text == "+/-")
            {
                if (isNumber(lastButtonText))
                {
                    if(lastButtonText[0] == '-'){
                        lastButtonText = lastButtonText.Substring(1);
                    }
                    else
                    {
                        lastButtonText = "-" + lastButtonText;
                    }
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
                else
                {
                    currentExpr += " " + lastButtonText;
                    lastButtonText = "-0";
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
            }
            else if(b.Text == "=")
            {
                try
                {
                    txtResult.Text = CalcTreeConsoleTest.CalcTree.ConvertStringToTree(txtInputExpr.Text).evaluate().ToString();
                }
                catch (Exception)
                {
                    txtResult.Text = "Error";
                }

                currentExpr = "";
                lastButtonText = "";
            }
            else
            {
                throw new InvalidOperationException("Invalid button pressed");
            }
        }

        private bool isNumber(String s)
        {
            try
            {
                double.Parse(s);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
    }
}
