using System.Collections.Generic;

namespace VisualCalculator.Calculator
{
    using static Calculator;

    static class Parser
    {
        public static void StringToInfixExpr(string expr, List<IObject> infixExpr)
        {
            // 앞에서부터 차례대로 파싱한다.
            for (int i = 0; i < expr.Length; ++i)
            {
                switch (GetValueType(expr[i]))
                {
                    case ValueType.NUMERIC:     i = ParseNumeric(expr, i, infixExpr);   break;
                    case ValueType.VARIABLE:    ParseVariable(expr, i, infixExpr);      break;
                    case ValueType.OPERATOR:    ParseOperator(expr, i, infixExpr);      break;
                    default:                    ParseBracket(expr, i, infixExpr);       break;
                        // 나머지 경우는 왼/오른 괄호의 경우만 남는다.(소수점은 숫자파싱에서 걸러진다.)
                }
            }
        }

        private static int ParseNumeric(string expr, int index, List<IObject> infixExpr)
        {
            // 소수점을 포함해 그 숫자가 끝날 때까지를 읽어서 파싱한다.
            int j = index;
            while (++j < expr.Length)
            {
                if (!(CheckValueType(expr[j], ValueType.NUMERIC)
                        || CheckValueType(expr[j], ValueType.DECIMAL)))
                    break;
            }
            var value = double.Parse(expr.Substring(index, j - index));
            infixExpr.Add(new Operand.Numeric(value));
            return j - 1;
        }

        private static void ParseVariable(string expr, int index, List<IObject> infixExpr)
        {
            // 변수의 바로 앞에 숫자가 있는 경우 곱셈연산자가 생략된 것이므로,
            // 곱셈 연산자를 추가해준 후 변수를 추가한다. (예: 3x + 2y => 3*x + 2*y)
            if (index > 0 && CheckValueType(expr[index - 1], ValueType.NUMERIC))
            {
                infixExpr.Add(new Operator.Binary.Mult());
            }
            infixExpr.Add(new Operand.Variable(expr[index]));
        }

        private static void ParseOperator(string expr, int index, List<IObject> infixExpr)
        {
            // '-'의 경우 뺄셈과 음수화의 두 가지 경우가 있다.
            switch (expr[index])
            {
                case '+': infixExpr.Add(new Operator.Binary.Plus());    break;
                case '*': infixExpr.Add(new Operator.Binary.Mult());    break;
                case '/': infixExpr.Add(new Operator.Binary.Div());     break;
                case '-':
                    if (index == 0     // 가장 앞인 경우(예: '-'x + y)
                        || (index > 0  // 바로 앞이 또 연산자이거나 왼쪽괄호인 경우(예: x'*-'y , x*'(-'y + z))
                            && (CheckValueType(expr[index - 1], ValueType.OPERATOR)
                                || CheckValueType(expr[index - 1], ValueType.BRACKET_LEFT))))
                    {
                        infixExpr.Add(new Operator.Unary.Negation());
                    }
                    else
                    {
                        infixExpr.Add(new Operator.Binary.Minus());
                    }
                    break;
            }
        }

        private static void ParseBracket(string expr, int index, List<IObject> infixExpr)
        {
            switch (expr[index])
            {
                case '(':
                    if (index > 0   // 바로 앞이 숫자/변수인 경우 곱셈연산이 생략된 것이므로 추가해준다.
                        && (CheckValueType(expr[index - 1], ValueType.NUMERIC)
                            || CheckValueType(expr[index - 1], ValueType.VARIABLE)))
                    {
                        infixExpr.Add(new Operator.Binary.Mult());
                    }
                    infixExpr.Add(new Operator.BracketL());
                    break;
                case ')':
                    infixExpr.Add(new Operator.BracketR());
                    break;
            }
        }
    }
}
