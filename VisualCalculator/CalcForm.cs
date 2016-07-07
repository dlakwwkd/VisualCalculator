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



        private enum ValueType
        {
            NUMERIC,
            VARIABLE,
            OPERATOR,
            DECIMAL,
            BRACKET_LEFT,
            BRACKET_RIGHT,
        }

        private void AddValue(char _value, ValueType _type)
        {
            if (expression.Text == "0")
                expression.Text = "";

            if (!CheckAddAble(_type))
                return;

            expression.Text += _value;
        }

        private void RemoveValue()
        {
            if (expression.Text.Length > 0)
            {
                if (IsLastValue(ValueType.BRACKET_LEFT))
                {
                    --bracketStack_;
                }
                else if (IsLastValue(ValueType.BRACKET_RIGHT))
                {
                    ++bracketStack_;
                }
                else if (IsLastValue(ValueType.DECIMAL))
                {
                    decimalUsed_ = false;
                }
                expression.Text = expression.Text.Substring(0, expression.Text.Length - 1);
            }
        }

        private bool IsLastValue(ValueType _type)
        {
            try { return valueKind_[(int)_type].Contains(expression.Text.Last()); }
            catch { return false; }
        }

        private bool CheckAddAble(ValueType _type)
        {
            switch (_type)
            {
                case ValueType.NUMERIC:
                    if (IsLastValue(ValueType.VARIABLE))
                        return false;

                    break;
                case ValueType.OPERATOR:
                    if (IsLastValue(ValueType.OPERATOR)
                        || IsLastValue(ValueType.DECIMAL)
                        || IsLastValue(ValueType.BRACKET_LEFT))
                        return false;

                    decimalUsed_ = false;
                    break;
                case ValueType.VARIABLE:
                    if (IsLastValue(ValueType.VARIABLE)
                        || IsLastValue(ValueType.DECIMAL))
                        return false;

                    break;
                case ValueType.DECIMAL:
                    if (decimalUsed_
                        || !IsLastValue(ValueType.NUMERIC))
                        return false;

                    decimalUsed_ = true;
                    break;
                case ValueType.BRACKET_LEFT:
                    if (IsLastValue(ValueType.DECIMAL))
                        return false;

                    decimalUsed_ = false;
                    ++bracketStack_;
                    break;
                case ValueType.BRACKET_RIGHT:
                    if (bracketStack_ < 1
                        || IsLastValue(ValueType.BRACKET_LEFT)
                        || IsLastValue(ValueType.DECIMAL))
                        return false;

                    decimalUsed_ = false;
                    --bracketStack_;
                    break;
            }
            return true;
        }

        private void num0_Click(object sender, EventArgs e)         { AddValue('0', ValueType.NUMERIC); }
        private void num1_Click(object sender, EventArgs e)         { AddValue('1', ValueType.NUMERIC); }
        private void num2_Click(object sender, EventArgs e)         { AddValue('2', ValueType.NUMERIC); }
        private void num3_Click(object sender, EventArgs e)         { AddValue('3', ValueType.NUMERIC); }
        private void num4_Click(object sender, EventArgs e)         { AddValue('4', ValueType.NUMERIC); }
        private void num5_Click(object sender, EventArgs e)         { AddValue('5', ValueType.NUMERIC); }
        private void num6_Click(object sender, EventArgs e)         { AddValue('6', ValueType.NUMERIC); }
        private void num7_Click(object sender, EventArgs e)         { AddValue('7', ValueType.NUMERIC); }
        private void num8_Click(object sender, EventArgs e)         { AddValue('8', ValueType.NUMERIC); }
        private void num9_Click(object sender, EventArgs e)         { AddValue('9', ValueType.NUMERIC); }
        private void plus_Click(object sender, EventArgs e)         { AddValue('+', ValueType.OPERATOR); }
        private void minus_Click(object sender, EventArgs e)        { AddValue('-', ValueType.OPERATOR); }
        private void mult_Click(object sender, EventArgs e)         { AddValue('*', ValueType.OPERATOR); }
        private void div_Click(object sender, EventArgs e)          { AddValue('/', ValueType.OPERATOR); }
        private void x_Click(object sender, EventArgs e)            { AddValue('x', ValueType.VARIABLE); }
        private void y_Click(object sender, EventArgs e)            { AddValue('y', ValueType.VARIABLE); }
        private void z_Click(object sender, EventArgs e)            { AddValue('z', ValueType.VARIABLE); }
        private void dot_Click(object sender, EventArgs e)          { AddValue('.', ValueType.DECIMAL); }
        private void leftBracket_Click(object sender, EventArgs e)  { AddValue('(', ValueType.BRACKET_LEFT); }
        private void rightBracket_Click(object sender, EventArgs e) { AddValue(')', ValueType.BRACKET_RIGHT); }
        private void erase_Click(object sender, EventArgs e)        { RemoveValue(); }
        private void ce_Click(object sender, EventArgs e)           { SetExpression("0"); bracketStack_ = 0; }
        private void c_Click(object sender, EventArgs e)            { SetExpression("0"); bracketStack_ = 0; }
        private void enter_Click(object sender, EventArgs e)        { calculator_.Calculate(); bracketStack_ = 0; }
        private void negation_Click(object sender, EventArgs e)
        {
        }

        private void CalcForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.Oemtilde: negation_Click(sender, e);      break;
                    case Keys.Oemplus:  plus_Click(sender, e);          break;
                    case Keys.D8:       mult_Click(sender, e);          break;
                    case Keys.D9:       leftBracket_Click(sender, e);   break;
                    case Keys.D0:       rightBracket_Click(sender, e);  break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.NumPad0:  case Keys.D0:           num0_Click(sender, e);  break;
                    case Keys.NumPad1:  case Keys.D1:           num1_Click(sender, e);  break;
                    case Keys.NumPad2:  case Keys.D2:           num2_Click(sender, e);  break;
                    case Keys.NumPad3:  case Keys.D3:           num3_Click(sender, e);  break;
                    case Keys.NumPad4:  case Keys.D4:           num4_Click(sender, e);  break;
                    case Keys.NumPad5:  case Keys.D5:           num5_Click(sender, e);  break;
                    case Keys.NumPad6:  case Keys.D6:           num6_Click(sender, e);  break;
                    case Keys.NumPad7:  case Keys.D7:           num7_Click(sender, e);  break;
                    case Keys.NumPad8:  case Keys.D8:           num8_Click(sender, e);  break;
                    case Keys.NumPad9:  case Keys.D9:           num9_Click(sender, e);  break;
                    case Keys.Add:      /* Shift + Oemplus */   plus_Click(sender, e);  break;
                    case Keys.Subtract: case Keys.OemMinus:     minus_Click(sender, e); break;
                    case Keys.Multiply: /* Shift + D8 */        mult_Click(sender, e);  break;
                    case Keys.Divide:   case Keys.OemQuestion:  div_Click(sender, e);   break;
                    case Keys.Decimal:  case Keys.OemPeriod:    dot_Click(sender, e);   break;
                    case Keys.X:                                x_Click(sender, e);     break;
                    case Keys.Y:                                y_Click(sender, e);     break;
                    case Keys.Z:                                z_Click(sender, e);     break;
                    case Keys.Back:                             erase_Click(sender, e); break;
                    case Keys.Delete:                           ce_Click(sender, e);    break;
                    case Keys.Escape:                           c_Click(sender, e);     break;
                    case Keys.Enter:    case Keys.Oemplus:      enter_Click(sender, e); break;
                }
            }
        }

        private Calculator.Calculator   calculator_ = new Calculator.Calculator();
        private string[]                valueKind_  =
        {
            "0123456789",   // NUMERIC
            "xyz",          // VARIABLE
            "+-*/",         // OPERATOR
            ".",            // DECIMAL
            "(",            // BRACKET_LEFT
            ")",            // BRACKET_RIGHT
        };
        private int                     bracketStack_   = 0;
        private bool                    decimalUsed_    = false;
    }
}
