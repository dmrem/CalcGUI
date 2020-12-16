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

                else
                {
                    currentExpr += " " + lastButtonText;
                    lastButtonText = b.Text;
                    txtInputExpr.Text = (currentExpr + " " + lastButtonText).Trim();
                }
            }
            else if(b.Text == "(" || b.Text == ")")
            {

            }
            else if(b.Text == "+" || b.Text == "-" || b.Text == "*" || b.Text == "/" || b.Text == "^")
            {
                if(currentExpr == "")
                {
                    lastButtonText = b.Text;
                    currentExpr = "0";
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
