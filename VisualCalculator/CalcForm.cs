using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualCalculator
{
    public partial class CalcForm : Form
    {
        public CalcForm()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(CalcForm_KeyDown);
        }

        public void SetExpression(string _expression)
        {
            expression.Text = _expression;
        }

        private void AddValue(char _value)
        {
            if (expression.Text == "0")
            {
                expression.Text = "";
            }
            expression.Text += _value;
        }

        private void num0_Click(object sender, EventArgs e) { AddValue('0'); }
        private void num1_Click(object sender, EventArgs e) { AddValue('1'); }
        private void num2_Click(object sender, EventArgs e) { AddValue('2'); }
        private void num3_Click(object sender, EventArgs e) { AddValue('3'); }
        private void num4_Click(object sender, EventArgs e) { AddValue('4'); }
        private void num5_Click(object sender, EventArgs e) { AddValue('5'); }
        private void num6_Click(object sender, EventArgs e) { AddValue('6'); }
        private void num7_Click(object sender, EventArgs e) { AddValue('7'); }
        private void num8_Click(object sender, EventArgs e) { AddValue('8'); }
        private void num9_Click(object sender, EventArgs e) { AddValue('9'); }
        private void plus_Click(object sender, EventArgs e) { AddValue('+'); }
        private void minus_Click(object sender, EventArgs e) { AddValue('-'); }
        private void mult_Click(object sender, EventArgs e) { AddValue('*'); }
        private void div_Click(object sender, EventArgs e) { AddValue('/'); }
        private void dot_Click(object sender, EventArgs e) { AddValue('.'); }
        private void erase_Click(object sender, EventArgs e) { }
        private void ce_Click(object sender, EventArgs e) { SetExpression("0"); }
        private void c_Click(object sender, EventArgs e) { SetExpression("0"); }
        private void enter_Click(object sender, EventArgs e) { calculator_.Calculate(); }
        private void negation_Click(object sender, EventArgs e) { }

        private void CalcForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.NumPad0:  num0_Click(sender, e);  break;
                case Keys.NumPad1:  num1_Click(sender, e);  break;
                case Keys.NumPad2:  num2_Click(sender, e);  break;
                case Keys.NumPad3:  num3_Click(sender, e);  break;
                case Keys.NumPad4:  num4_Click(sender, e);  break;
                case Keys.NumPad5:  num5_Click(sender, e);  break;
                case Keys.NumPad6:  num6_Click(sender, e);  break;
                case Keys.NumPad7:  num7_Click(sender, e);  break;
                case Keys.NumPad8:  num8_Click(sender, e);  break;
                case Keys.NumPad9:  num9_Click(sender, e);  break;
                case Keys.Add:      plus_Click(sender, e);  break;
                case Keys.Subtract: minus_Click(sender, e); break;
                case Keys.Multiply: mult_Click(sender, e);  break;
                case Keys.Divide:   div_Click(sender, e);   break;
                case Keys.Decimal:  dot_Click(sender, e);   break;
                case Keys.Back:     erase_Click(sender, e); break;
                case Keys.Delete:   ce_Click(sender, e);    break;
                case Keys.Escape:   c_Click(sender, e);     break;
                case Keys.Enter:    enter_Click(sender, e); break;
            }
        }

        private Calculator.Calculator calculator_ = new Calculator.Calculator();
    }
}
