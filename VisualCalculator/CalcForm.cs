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
        public enum ValueType
        {
            NUMERIC,
            VARIABLE,
            OPERATOR,
            DECIMAL,
            BRACKET_LEFT,
            BRACKET_RIGHT,
        }
        public static bool CheckValueType(char _value, ValueType _type)
        {
            try { return VALUE_KINDS[(int)_type].Contains(_value); }
            catch { return false; }
        }
        private static string[] VALUE_KINDS =
        {
            "0123456789",   // NUMERIC
            "xyz",          // VARIABLE
            "+-*/",         // OPERATOR
            ".",            // DECIMAL
            "(",            // BRACKET_LEFT
            ")",            // BRACKET_RIGHT
        };



        public CalcForm()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(CalcForm_KeyDown);
        }

        public void SetExpression(string _expression)
        {
            expression.Text = _expression;
        }



        private void AddValue(char _value, ValueType _type)
        {
            if (expression.Text == "0" && _type != ValueType.DECIMAL)
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

        private bool CheckAddAble(ValueType _type)
        {
            switch (_type)
            {
                case ValueType.NUMERIC:
                    if (IsLastValue(ValueType.VARIABLE)
                        || IsLastValue(ValueType.BRACKET_RIGHT))
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
                        || IsLastValue(ValueType.DECIMAL)
                        || IsLastValue(ValueType.BRACKET_RIGHT))
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

        private bool IsLastValue(ValueType _type)
        {
            try { return CheckValueType(expression.Text.Last(), _type); }
            catch { return false; }
        }

        private void num0_Click(object sender, EventArgs e)     { AddValue('0', ValueType.NUMERIC); }
        private void num1_Click(object sender, EventArgs e)     { AddValue('1', ValueType.NUMERIC); }
        private void num2_Click(object sender, EventArgs e)     { AddValue('2', ValueType.NUMERIC); }
        private void num3_Click(object sender, EventArgs e)     { AddValue('3', ValueType.NUMERIC); }
        private void num4_Click(object sender, EventArgs e)     { AddValue('4', ValueType.NUMERIC); }
        private void num5_Click(object sender, EventArgs e)     { AddValue('5', ValueType.NUMERIC); }
        private void num6_Click(object sender, EventArgs e)     { AddValue('6', ValueType.NUMERIC); }
        private void num7_Click(object sender, EventArgs e)     { AddValue('7', ValueType.NUMERIC); }
        private void num8_Click(object sender, EventArgs e)     { AddValue('8', ValueType.NUMERIC); }
        private void num9_Click(object sender, EventArgs e)     { AddValue('9', ValueType.NUMERIC); }
        private void plus_Click(object sender, EventArgs e)     { AddValue('+', ValueType.OPERATOR); }
        private void minus_Click(object sender, EventArgs e)    { AddValue('-', ValueType.OPERATOR); }
        private void mult_Click(object sender, EventArgs e)     { AddValue('*', ValueType.OPERATOR); }
        private void div_Click(object sender, EventArgs e)      { AddValue('/', ValueType.OPERATOR); }
        private void x_Click(object sender, EventArgs e)        { AddValue('x', ValueType.VARIABLE); }
        private void y_Click(object sender, EventArgs e)        { AddValue('y', ValueType.VARIABLE); }
        private void z_Click(object sender, EventArgs e)        { AddValue('z', ValueType.VARIABLE); }
        private void dot_Click(object sender, EventArgs e)      { AddValue('.', ValueType.DECIMAL); }
        private void bracketL_Click(object sender, EventArgs e) { AddValue('(', ValueType.BRACKET_LEFT); }
        private void bracketR_Click(object sender, EventArgs e) { AddValue(')', ValueType.BRACKET_RIGHT); }
        private void erase_Click(object sender, EventArgs e)    { RemoveValue(); }
        private void ce_Click(object sender, EventArgs e)       { SetExpression("0"); bracketStack_ = 0; }
        private void c_Click(object sender, EventArgs e)        { SetExpression("0"); bracketStack_ = 0; }
        private void enter_Click(object sender, EventArgs e)
        {
            bracketStack_ = 0;
            calculator_.Calculate(expression.Text);
        }
        private void negation_Click(object sender, EventArgs e)
        {
            var expr = expression.Text;
            int idx = expr.Length - 1;
            if (IsLastValue(ValueType.NUMERIC) || IsLastValue(ValueType.VARIABLE))
            {
                // 숫자인 경우, 해당 숫자의 앞부분의 위치를 idx에 저장한다.
                // 변수인 경우, 변수의 앞부분 또는 변수와 곱셈연산생략으로 붙어있는 숫자의 앞부분을 찾아낸다.
                // [예: 32.01 => -32.01 , x => -x , 24y => -24y]
                while (--idx > 0)
                {
                    if (CheckValueType(expr[idx], ValueType.NUMERIC)
                        || CheckValueType(expr[idx], ValueType.DECIMAL))
                        continue;

                    ++idx;
                    break;
                }
            }
            else if (IsLastValue(ValueType.BRACKET_RIGHT))
            {
                // 오른쪽 괄호인 경우, 왼괄호를 찾아 앞부분으로 탐색해나가면서
                // 왼괄호 전에 오른괄호가 또 나온다면, 그만큼 왼괄호를 생략해줘야 같은 범위의 왼괄호를 찾을 수 있다.
                // [예: x*(y + z) => x*-(y + z) , x*(y/(z + x)) => x*-(y/(z + x))]
                int bracketStack = 1;
                while (--idx > 0)
                {
                    if (CheckValueType(expr[idx], ValueType.BRACKET_RIGHT))
                        ++bracketStack;

                    if (CheckValueType(expr[idx], ValueType.BRACKET_LEFT)
                        && --bracketStack == 0) // 이 연산 순서가 바뀌면 안되는 점에 유의(왼괄호 일치확인-> 스택감산-> 0인지 확인)
                    {
                        if (!CheckValueType(expr[idx - 1], ValueType.BRACKET_LEFT)
                            && !CheckValueType(expr[idx - 1], ValueType.OPERATOR))
                        {
                            // 왼괄호 바로 앞이 왼괄호와 연산자가 아닌 경우(즉, 숫자,변수,오른괄호)에는
                            // 곱셈연산이 생략된 것이므로, 음수화 전에 곱셈연산을 명시해준다.
                            // [예: 3(x + y) => 3*-(x + y) , x(y + z) => x*-(y +z) , (x + y)(y + z) => (x + y)*-(y + z)]
                            expr = expr.Insert(idx, "*");
                            ++idx;
                        }
                        break;
                    }
                }
            }
            else
            {
                // 나머지는 소수점,왼괄호의 경우 또는 식에 값이 하나도 없는 경우인데
                // 이 때는 음수화연산이 실행되면 안 되므로 함수를 종료한다.
                return;
            }

            // 식에 값이 하나인 경우였다면, idx가 -1이 되는데, 이를 0으로 바꿔줘야 밑의 연산이 문제없이 된다.
            if (idx < 0)
                idx = 0;

            // 음수화부호가 이미 있다면 새로 추가하지 않고 제거한다.
            if (idx == 0 && expr[idx] == '-')
            {
                // [예: -x => x]
                expression.Text = expr.Remove(idx, 1);
            }
            else if (idx > 1 && expr[idx - 1] == '-'
                && (CheckValueType(expr[idx - 2], ValueType.OPERATOR)
                    || CheckValueType(expr[idx - 2], ValueType.BRACKET_LEFT)))
            {
                // [예: x*-y => x*y , (-x... => (x...]
                expression.Text = expr.Remove(idx - 1, 1);
            }
            else
            {
                // 나머지 경우는 음수화부호를 추가해줘야 하는 경우이다.
                expression.Text = expr.Insert(idx, "-");
            }
        }

        private void CalcForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.Oemtilde: negation_Click(sender, e);  break;
                    case Keys.Oemplus:  plus_Click(sender, e);      break;
                    case Keys.D8:       mult_Click(sender, e);      break;
                    case Keys.D9:       bracketL_Click(sender, e);  break;
                    case Keys.D0:       bracketR_Click(sender, e);  break;
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

        private Calculator.Calculator   calculator_     = new Calculator.Calculator();
        private bool                    decimalUsed_    = false;
        private int                     bracketStack_   = 0;
    }
}
