using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Calculator
{
    static class Parser
    {
        public static void StringToInfixExpr(string _expr, List<IObject> _infixExpr)
        {
            // 앞에서부터 차례대로 파싱한다.
            for (int i = 0; i < _expr.Length; ++i)
            {
                switch (Calculator.GetValueType(_expr[i]))
                {
                    case Calculator.ValueType.NUMERIC:  i = ParseNumeric(_expr, i, _infixExpr); break;
                    case Calculator.ValueType.VARIABLE: ParseVariable(_expr, i, _infixExpr);    break;
                    case Calculator.ValueType.OPERATOR: ParseOperator(_expr, i, _infixExpr);    break;
                    default:                            ParseBracket(_expr, i, _infixExpr);     break;
                        // 나머지 경우는 왼/오른 괄호의 경우만 남는다.(소수점은 숫자파싱에서 걸러진다.)
                }
            }
        }



        private static int ParseNumeric(string _expr, int _index, List<IObject> _infixExpr)
        {
            // 소수점을 포함해 그 숫자가 끝날 때까지를 읽어서 파싱한다.
            int j = _index;
            while (++j < _expr.Length)
            {
                if (!(Calculator.CheckValueType(_expr[j], Calculator.ValueType.NUMERIC)
                    || Calculator.CheckValueType(_expr[j], Calculator.ValueType.DECIMAL)))
                    break;
            }
            var value = double.Parse(_expr.Substring(_index, j - _index));
            _infixExpr.Add(new Operand.Numeric(value));
            return j - 1;
        }

        private static void ParseVariable(string _expr, int _index, List<IObject> _infixExpr)
        {
            // 변수의 바로 앞에 숫자가 있는 경우 곱셈연산자가 생략된 것이므로,
            // 곱셈 연산자를 추가해준 후 변수를 추가한다. (예: 3x + 2y => 3*x + 2*y)
            if (_index > 0 && Calculator.CheckValueType(_expr[_index - 1], Calculator.ValueType.NUMERIC))
            {
                _infixExpr.Add(new Operator.Binary.Mult());
            }
            _infixExpr.Add(new Operand.Variable(_expr[_index]));
        }

        private static void ParseOperator(string _expr, int _index, List<IObject> _infixExpr)
        {
            // '-'의 경우 뺄셈과 음수화의 두 가지 경우가 있다.
            switch (_expr[_index])
            {
                case '+': _infixExpr.Add(new Operator.Binary.Plus()); break;
                case '*': _infixExpr.Add(new Operator.Binary.Mult()); break;
                case '/': _infixExpr.Add(new Operator.Binary.Div()); break;
                case '-':
                    if (_index == 0      // 가장 앞인 경우(예: '-'x + y)
                        || _index > 0    // 바로 앞이 또 연산자이거나 왼쪽괄호인 경우(예: x'*-'y , x*'(-'y + z))
                        && (Calculator.CheckValueType(_expr[_index - 1], Calculator.ValueType.OPERATOR)
                            || Calculator.CheckValueType(_expr[_index - 1], Calculator.ValueType.OPERATOR)))
                    {
                        _infixExpr.Add(new Operator.Unary.Negation());
                    }
                    else
                    {
                        _infixExpr.Add(new Operator.Binary.Minus());
                    }
                    break;
            }
        }

        private static void ParseBracket(string _expr, int _index, List<IObject> _infixExpr)
        {
            switch (_expr[_index])
            {
                case '(':
                    if (_index > 0   // 바로 앞이 숫자/변수인 경우 곱셈연산이 생략된 것이므로 추가해준다.
                        && (Calculator.CheckValueType(_expr[_index - 1], Calculator.ValueType.NUMERIC)
                            || Calculator.CheckValueType(_expr[_index - 1], Calculator.ValueType.VARIABLE)))
                    {
                        _infixExpr.Add(new Operator.Binary.Mult());
                    }
                    _infixExpr.Add(new Operator.BracketL());
                    break;
                case ')':
                    _infixExpr.Add(new Operator.BracketR());
                    break;
            }
        }
    }
}
