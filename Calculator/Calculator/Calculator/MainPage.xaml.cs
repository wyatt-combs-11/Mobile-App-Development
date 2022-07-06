using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        bool multOp;
        int num1, num2;
        string op;
        public MainPage()
        {
            InitializeComponent();
            op = "";
            num1 = num2 = 0;
            multOp = false;
        }

        private void OnClicked(object sender, EventArgs e)
        {
            if (multOp)
            {
                Calc.Text = "0";
                multOp = false;
            }

            Button button = (Button)sender;
            if (Calc.Text.Equals("0") && button.Text.Equals("0"))
                return;
            if(Calc.Text.Equals("0"))
                Calc.Text = button.Text;
            else
                Calc.Text += button.Text;
        }

        private void OperationClicked(object sender, EventArgs e)
        {
            if (!op.Equals(""))
            {
                Calculate();
                multOp = true;
                num1 = Int32.Parse(Calc.Text);
            } else
            {
                num1 = Int32.Parse(Calc.Text);
                Calc.Text = "0";
            }

            op = ((Button)sender).Text;
        }

        private void PercentClicked(object sender, EventArgs e)
        {
            Calc.Text = "" + (Int32.Parse(Calc.Text) / 100);
        }

        private void Calculate()
        {
            try
            {
                num2 = Int32.Parse(Calc.Text);
                switch (op)
                {
                    case "+":
                        Calc.Text = "" + (num1 + num2);
                        Debug.WriteLine("Plus, " + num1 + ", " + num2);
                        break;
                    case "-":
                        Calc.Text = "" + (num1 - num2);
                        Debug.WriteLine("Sub, " + num1 + ", " + num2);
                        break;
                    case "x":
                        Calc.Text = "" + (num1 * num2);
                        Debug.WriteLine("Mult, " + num1 + ", " + num2);
                        break;
                    case "÷":
                        Calc.Text = "" + (num1 / num2);
                        Debug.WriteLine("Div, " + num1 + ", " + num2);
                        break;
                    case "%":
                    default:
                        Debug.WriteLine("Fail, " + num1 + ", " + num2);
                        break;
                }
                num1 = Int32.Parse(Calc.Text);
                num2 = 0;
            } catch(Exception e)
            {
                Calc.Text = "0";
                num1 = num2 = 0;
                op = "";
            }
            
        }

        private void EqualsClicked(object sender, EventArgs e)
        {
            Calculate();
            op = "";
        }

        private void ChangeSigns(object sender, EventArgs e)
        {
            Calc.Text = "" + (Int32.Parse(Calc.Text) * -1);
        }

        private void ClearClicked(object sender, EventArgs e)
        {
            Calc.Text = "0";
            num1 = num2 = 0;
            op = "";
        }
    }
}
