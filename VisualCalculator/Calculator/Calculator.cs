using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualCalculator.Calculator
{
    class Calculator
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

        //------------------------------------------------------------------------------------
        // Static Field
        //------------------------------------------------------------------------------------
        public static bool CheckValueType(char _value, ValueType _type)
        {
            try { return VALUE_KINDS[(int)_type].Contains(_value); }
            catch { return false; }
        }

        public static ValueType GetValueType(char _value)
        {
            foreach (ValueType type in Enum.GetValues(typeof(ValueType)))
            {
                if (CheckValueType(_value, type))
                    return type;
            }
            throw new ArgumentException("invalid argument");
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



        //------------------------------------------------------------------------------------
        // Public Field
        //------------------------------------------------------------------------------------
        public Calculator(CalcForm _form)
        {
            form_ = _form;
            bracketStack_ = 0;
            decimalUsed_ = false;
            sya_ = new ShuntingYardAlgorithm();
            exprTree_ = new ExpressionTree();
        }

        public void Init()
        {
            form_.Expr = "0";
            bracketStack_ = 0;
            decimalUsed_ = false;
        }

        public void AddValue(char _value)
        {
            try
            {
                var type = GetValueType(_value);
                if (form_.Expr == "0" && type != ValueType.DECIMAL)
                    form_.Expr = "";

                if (CheckAddAble(type))
                {
                    if (type == ValueType.NUMERIC)
                    {
                        if (!decimalUsed_
                            && _value == '0'
                            && form_.Expr.Any()
                            && form_.Expr.Last() == '0')
                            return;
                    }
                    else
                    {
                        decimalUsed_ = false;
                    }
                    switch (type)
                    {
                        case ValueType.DECIMAL: decimalUsed_ = true; break;
                        case ValueType.BRACKET_LEFT: ++bracketStack_; break;
                        case ValueType.BRACKET_RIGHT: --bracketStack_; break;
                    }
                    form_.Expr += _value;
                }
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }

        public void RemoveValue()
        {
            var expr = form_.Expr;
            if (expr.Any())
            {
                try
                {
                    switch (GetValueType(expr.Last()))
                    {
                        case ValueType.DECIMAL: decimalUsed_ = false; break;
                        case ValueType.BRACKET_LEFT: --bracketStack_; break;
                        case ValueType.BRACKET_RIGHT: ++bracketStack_; break;
                    }
                    form_.Expr = expr.Substring(0, expr.Length - 1);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
        }

        public void NegationProc()
        {
            var expr = form_.Expr;
            if (!expr.Any())
                return;

            ValueType lastValueType;
            try { lastValueType = GetValueType(expr.Last()); }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return;
            }

            int idx = expr.Length - 1;
            switch (lastValueType)
            {
                case ValueType.NUMERIC:
                case ValueType.VARIABLE:
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
                    break;

                case ValueType.BRACKET_RIGHT:
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
                    break;

                default:
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
                form_.Expr = expr.Remove(idx, 1);
            }
            else if (idx > 1 && expr[idx - 1] == '-'
                && (CheckValueType(expr[idx - 2], ValueType.OPERATOR)
                    || CheckValueType(expr[idx - 2], ValueType.BRACKET_LEFT)))
            {
                // [예: x*-y => x*y , (-x... => (x...]
                form_.Expr = expr.Remove(idx - 1, 1);
            }
            else
            {
                // 나머지 경우는 음수화부호를 추가해줘야 하는 경우이다.
                form_.Expr = expr.Insert(idx, "-");
            }
        }

        public void EnterProc()
        {
            if (!form_.Expr.Any()
                || bracketStack_ > 0
                || CheckValueType(form_.Expr.Last(), ValueType.OPERATOR)
                || CheckValueType(form_.Expr.Last(), ValueType.DECIMAL)
                || CheckValueType(form_.Expr.Last(), ValueType.BRACKET_LEFT))
                return;

            bracketStack_ = 0;
            decimalUsed_ = false;
            Run();
        }

        public async void CalcProc()
        {
            double x, y, z;
            if (form_.GetX(out x))
            {
                await exprTree_.SetVariable('x', x);
            }
            if (form_.GetY(out y))
            {
                await exprTree_.SetVariable('y', y);
            }
            if (form_.GetZ(out z))
            {
                await exprTree_.SetVariable('z', z);
            }
            form_.ResultPanel.Enabled = false;
            form_.SetResult(await exprTree_.Evaluate());
            form_.ResultPanel.Enabled = true;
        }

        public void ResetProc()
        {
            form_.TreePanel.Enabled = false;
            form_.ResultPanel.Enabled = false;
            form_.InputEnable = true;
            form_.InputPanel.Select();
        }


        //------------------------------------------------------------------------------------
        // Private Field
        //------------------------------------------------------------------------------------
        private bool CheckAddAble(ValueType _type)
        {
            var expr = form_.Expr;
            if (expr.Any())
                return CheckAddAble(_type, GetValueType(expr.Last()));

            switch (_type)
            {
                case ValueType.OPERATOR:
                case ValueType.DECIMAL:
                    return false;

                default:
                    return true;
            }
        }

        private bool CheckAddAble(ValueType _input, ValueType _lastValue)
        {
            switch (_input)
            {
                case ValueType.NUMERIC:
                    return _lastValue == ValueType.VARIABLE
                        || _lastValue == ValueType.BRACKET_RIGHT
                        ? false : true;

                case ValueType.OPERATOR:
                    return _lastValue == ValueType.OPERATOR
                        || _lastValue == ValueType.DECIMAL
                        || _lastValue == ValueType.BRACKET_LEFT
                        ? false : true;

                case ValueType.VARIABLE:
                    return _lastValue == ValueType.VARIABLE
                        || _lastValue == ValueType.DECIMAL
                        || _lastValue == ValueType.BRACKET_RIGHT
                        ? false : true;

                case ValueType.DECIMAL:
                    return decimalUsed_
                        || _lastValue != ValueType.NUMERIC
                        ? false : true;

                case ValueType.BRACKET_LEFT:
                    return _lastValue == ValueType.DECIMAL
                        ? false : true;

                case ValueType.BRACKET_RIGHT:
                    return bracketStack_ < 1
                        || _lastValue == ValueType.DECIMAL
                        || _lastValue == ValueType.BRACKET_LEFT
                        ? false : true;

                default:
                    return false;
            }
        }

        private async void Run()
        {
            List<IObject> infixExpr = new List<IObject>();
            try
            {
                Parser.StringToInfixExpr(form_.Expr, infixExpr);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return;
            }
            form_.InputEnable = false;
            form_.SyaPanel.Enabled = true;

            var postfixExpr = await sya_.Run(form_.SyaPanel, infixExpr);

            form_.SyaPanel.Enabled = false;
            form_.TreePanel.Enabled = true;

            await exprTree_.BuildTree(form_.TreePanel, postfixExpr);

            form_.ResultPanel.Enabled = true;
        }



        private CalcForm                form_;
        private int                     bracketStack_;
        private bool                    decimalUsed_;

        private ShuntingYardAlgorithm   sya_;
        private ExpressionTree          exprTree_;
    }
}
