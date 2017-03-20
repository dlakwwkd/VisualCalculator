using System;
using System.Collections.Generic;
using System.Linq;

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
        public static bool CheckValueType(char value, ValueType type)
        {
            return ValueTypeMap[type].Contains(value);
        }

        public static ValueType GetValueType(char value)
        {
            foreach (ValueType type in Enum.GetValues(typeof(ValueType)))
            {
                if (CheckValueType(value, type))
                    return type;
            }
            throw new ArgumentException("invalid argument");
        }

        private static Dictionary<ValueType, string> ValueTypeMap { get; } = new Dictionary<ValueType, string>()
        {
            { ValueType.NUMERIC,        "0123456789"    },
            { ValueType.VARIABLE,       "xyz"           },
            { ValueType.OPERATOR,       "+-*/"          },
            { ValueType.DECIMAL,        "."             },
            { ValueType.BRACKET_LEFT,   "("             },
            { ValueType.BRACKET_RIGHT,  ")"             },
        };

        //------------------------------------------------------------------------------------
        // Public Field
        //------------------------------------------------------------------------------------
        public Calculator(CalcForm form)
        {
            Form = form;
        }

        public void Init()
        {
            Form.Expr       = "0";
            BracketStack    = 0;
            DecimalUsed     = false;
        }

        public void AddValue(char value)
        {
            try
            {
                var type = GetValueType(value);
                if (type != ValueType.DECIMAL && Form.Expr == "0")
                {
                    Form.Expr = "";
                }
                if (CheckAddAble(type))
                {
                    if (type == ValueType.NUMERIC)
                    {
                        if (!DecimalUsed
                            && value == '0'
                            && Form.Expr.Any()
                            && Form.Expr.Last() == '0')
                            return;
                    }
                    else
                    {
                        DecimalUsed = false;
                    }

                    switch (type)
                    {
                        case ValueType.DECIMAL:         DecimalUsed = true;     break;
                        case ValueType.BRACKET_LEFT:    ++BracketStack;         break;
                        case ValueType.BRACKET_RIGHT:   --BracketStack;         break;
                    }
                    Form.Expr += value;
                }
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }

        public void RemoveValue()
        {
            var expr = Form.Expr;
            if (expr.Any())
            {
                try
                {
                    switch (GetValueType(expr.Last()))
                    {
                        case ValueType.DECIMAL:         DecimalUsed = false;    break;
                        case ValueType.BRACKET_LEFT:    --BracketStack;         break;
                        case ValueType.BRACKET_RIGHT:   ++BracketStack;         break;
                    }
                    Form.Expr = expr.Substring(0, expr.Length - 1);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
        }

        public void NegationProc()
        {
            var expr = Form.Expr;
            if (!expr.Any())
                return;

            ValueType lastValueType;
            try
            {
                lastValueType = GetValueType(expr.Last());
            }
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
                        {
                            ++bracketStack;
                        }
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
                Form.Expr = expr.Remove(idx, 1);
            }
            else if (idx > 1 && expr[idx - 1] == '-'
                && (CheckValueType(expr[idx - 2], ValueType.OPERATOR)
                    || CheckValueType(expr[idx - 2], ValueType.BRACKET_LEFT)))
            {
                // [예: x*-y => x*y , (-x... => (x...]
                Form.Expr = expr.Remove(idx - 1, 1);
            }
            else
            {
                // 나머지 경우는 음수화부호를 추가해줘야 하는 경우이다.
                Form.Expr = expr.Insert(idx, "-");
            }
        }

        public void EnterProc()
        {
            if (!Form.Expr.Any()
                || BracketStack > 0
                || CheckValueType(Form.Expr.Last(), ValueType.OPERATOR)
                || CheckValueType(Form.Expr.Last(), ValueType.DECIMAL)
                || CheckValueType(Form.Expr.Last(), ValueType.BRACKET_LEFT))
                return;

            BracketStack = 0;
            DecimalUsed = false;
            Run();
        }

        public async void CalcProc()
        {
            if (Form.GetX(out double x))
            {
                await ExprTree.SetVariable('x', x);
            }
            if (Form.GetY(out double y))
            {
                await ExprTree.SetVariable('y', y);
            }
            if (Form.GetZ(out double z))
            {
                await ExprTree.SetVariable('z', z);
            }
            Form.ResultPanel.Enabled = false;
            Form.SetResult(await ExprTree.Evaluate());
            Form.ResultPanel.Enabled = true;
        }

        public void ResetProc()
        {
            Form.ResultPanel.Enabled = false;
            Form.TreePanel.Enabled = false;
            Form.InputEnable = true;
            Form.InputPanel.Select();
        }

        //------------------------------------------------------------------------------------
        // Private Field
        //------------------------------------------------------------------------------------
        private bool CheckAddAble(ValueType type)
        {
            var expr = Form.Expr;
            if (expr.Any())
                return CheckAddAble(type, GetValueType(expr.Last()));

            switch (type)
            {
                case ValueType.OPERATOR:
                case ValueType.DECIMAL:
                    return false;

                default:
                    return true;
            }
        }

        private bool CheckAddAble(ValueType input, ValueType lastValue)
        {
            switch (input)
            {
                case ValueType.NUMERIC:
                    return lastValue == ValueType.VARIABLE
                        || lastValue == ValueType.BRACKET_RIGHT
                        ? false : true;

                case ValueType.OPERATOR:
                    return lastValue == ValueType.OPERATOR
                        || lastValue == ValueType.DECIMAL
                        || lastValue == ValueType.BRACKET_LEFT
                        ? false : true;

                case ValueType.VARIABLE:
                    return lastValue == ValueType.VARIABLE
                        || lastValue == ValueType.DECIMAL
                        || lastValue == ValueType.BRACKET_RIGHT
                        ? false : true;

                case ValueType.DECIMAL:
                    return DecimalUsed
                        || lastValue != ValueType.NUMERIC
                        ? false : true;

                case ValueType.BRACKET_LEFT:
                    return lastValue == ValueType.DECIMAL
                        ? false : true;

                case ValueType.BRACKET_RIGHT:
                    return BracketStack < 1
                        || lastValue == ValueType.DECIMAL
                        || lastValue == ValueType.BRACKET_LEFT
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
                Parser.StringToInfixExpr(Form.Expr, infixExpr);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return;
            }
            Form.InputEnable = false;
            Form.SyaPanel.Enabled = true;

            var postfixExpr = await Sya.Run(Form.SyaPanel, infixExpr);

            Form.SyaPanel.Enabled = false;
            Form.TreePanel.Enabled = true;

            await ExprTree.BuildTree(Form.TreePanel, postfixExpr);

            Form.ResultPanel.Enabled = true;
        }
        
        private CalcForm                Form            { get; }        = null;
        private ShuntingYardAlgorithm   Sya             { get; }        = new ShuntingYardAlgorithm();
        private ExpressionTree          ExprTree        { get; }        = new ExpressionTree();
        private int                     BracketStack    { get; set; }   = 0;
        private bool                    DecimalUsed     { get; set; }   = false;
    }
}
