using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Calculator
{
    class Calculator
    {
        public void Calculate(string _expression)
        {
            try { ParseInfixExprFromString(_expression); }
            catch { return; }

            sya_.MakePostfixFromInfix(infixExpression_);
        }

        public void SetStringFromResult(double _result)
        {

        }



        private void ParseInfixExprFromString(string _expression)
        {
            // 앞에서부터 차례대로 파싱한다.
            for (int i = 0; i < _expression.Length; ++i)
            {
                char curChar = _expression[i];
                if (CalcForm.CheckValueType(curChar, CalcForm.ValueType.NUMERIC))
                {
                    // 숫자라면, 소수점을 포함해 그 숫자가 끝날 때까지를 읽어서 파싱한다.
                    for (int j = i + 1; j < _expression.Length; ++j)
                    {
                        if (CalcForm.CheckValueType(_expression[j], CalcForm.ValueType.NUMERIC)
                            || CalcForm.CheckValueType(_expression[j], CalcForm.ValueType.DECIMAL))
                            continue;

                        var value = double.Parse(_expression.Substring(i, j - i));
                        infixExpression_.Add(new Operand.Numeric(value));
                        i = j - 1;
                        break;
                    }
                }
                else if (CalcForm.CheckValueType(curChar, CalcForm.ValueType.VARIABLE))
                {
                    // 변수라면, 변수의 바로 앞에 숫자가 있는 경우 곱셈연산자가 생략된 것이므로,
                    // 곱셈 연산자를 추가해준 후 변수를 추가한다. (예: 3x + 2y => 3*x + 2*y)
                    if (i > 0 && CalcForm.CheckValueType(_expression[i - 1], CalcForm.ValueType.NUMERIC))
                    {
                        infixExpression_.Add(new Operator.Binary.Mult());
                    }
                    infixExpression_.Add(new Operand.Variable(curChar));
                }
                else if (CalcForm.CheckValueType(curChar, CalcForm.ValueType.OPERATOR))
                {
                    // 연산자라면, '-'의 경우 뺄셈과 음수화의 두 가지 경우가 있으므로,
                    // 가장 앞인 경우와 바로 앞이 또 연산자이거나 왼쪽괄호인 경우가 음수화연산에 해당되므로,
                    // 이 두 경우에는 음수화 연산자로 파싱한다.
                    switch (curChar)
                    {
                        case '+': infixExpression_.Add(new Operator.Binary.Plus()); break;
                        case '*': infixExpression_.Add(new Operator.Binary.Mult()); break;
                        case '/': infixExpression_.Add(new Operator.Binary.Div()); break;
                        case '-':
                            if (i == 0      // 가장 앞인 경우(예: '-'x + y)
                                || i > 0    // 바로 앞이 또 연산자이거나 왼쪽괄호인 경우(예: x'*-'y , x*'(-'y + z))
                                && (CalcForm.CheckValueType(_expression[i - 1], CalcForm.ValueType.OPERATOR)
                                    || CalcForm.CheckValueType(_expression[i - 1], CalcForm.ValueType.OPERATOR)))
                                infixExpression_.Add(new Operator.Unary.Negation());
                            else
                                infixExpression_.Add(new Operator.Binary.Minus());
                            break;
                    }
                }
                else
                {
                    // 나머지 경우는 왼/오른 괄호의 경우만 남는다.(소수점은 숫자파싱에서 걸러진다.)
                    switch (curChar)
                    {
                        case '(':
                            if (i > 0   // 바로 앞이 숫자/변수인 경우 곱셈연산이 생략된 것이므로 추가해준다.
                                && (CalcForm.CheckValueType(_expression[i - 1], CalcForm.ValueType.NUMERIC)
                                    || CalcForm.CheckValueType(_expression[i - 1], CalcForm.ValueType.VARIABLE)))
                            {
                                infixExpression_.Add(new Operator.Binary.Mult());
                            }
                            infixExpression_.Add(new Operator.BracketL());
                            break;
                        case ')':
                            infixExpression_.Add(new Operator.BracketR());
                            break;
                    }
                }
            }
        }

        private List<object>            infixExpression_    = new List<object>();
        private ExpressionTree          exprTree_           = new ExpressionTree();
        private ShuntingYardAlgorithm   sya_                = new ShuntingYardAlgorithm();
    }
}
