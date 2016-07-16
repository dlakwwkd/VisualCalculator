using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Calculator
{
    class Calculator
    {
        //------------------------------------------------------------------------------------
        // Public Field
        //------------------------------------------------------------------------------------
        public async Task Run(string _expression)
        {
            try
            {
                ParseInfixExprFromString(_expression);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return;
            }

            await sya_.Run(infixExpression_);
        }

        public void SetStringFromResult(double _result)
        {

        }



        //------------------------------------------------------------------------------------
        // Private Field
        //------------------------------------------------------------------------------------
        private void ParseInfixExprFromString(string _expression)
        {
            // 앞에서부터 차례대로 파싱한다.
            for (int i = 0; i < _expression.Length; ++i)
            {
                switch (CalcForm.GetValueType(_expression[i]))
                {
                    case CalcForm.ValueType.NUMERIC:    i = ParseNumeric(_expression, i);   break;
                    case CalcForm.ValueType.VARIABLE:   ParseVariable(_expression, i);      break;
                    case CalcForm.ValueType.OPERATOR:   ParseOperator(_expression, i);      break;
                    default:                            ParseBracket(_expression, i);       break;
                        // 나머지 경우는 왼/오른 괄호의 경우만 남는다.(소수점은 숫자파싱에서 걸러진다.)
                }
            }
        }

        private int ParseNumeric(string _expr, int _index)
        {
            // 소수점을 포함해 그 숫자가 끝날 때까지를 읽어서 파싱한다.
            for (int j = _index + 1; j < _expr.Length; ++j)
            {
                if (CalcForm.CheckValueType(_expr[j], CalcForm.ValueType.NUMERIC)
                    || CalcForm.CheckValueType(_expr[j], CalcForm.ValueType.DECIMAL))
                    continue;

                var value = double.Parse(_expr.Substring(_index, j - _index));
                infixExpression_.Add(new Operand.Numeric(value));
                return j - 1;
            }
            return _index;
        }

        private void ParseVariable(string _expr, int _index)
        {
            // 변수의 바로 앞에 숫자가 있는 경우 곱셈연산자가 생략된 것이므로,
            // 곱셈 연산자를 추가해준 후 변수를 추가한다. (예: 3x + 2y => 3*x + 2*y)
            if (_index > 0 && CalcForm.CheckValueType(_expr[_index - 1], CalcForm.ValueType.NUMERIC))
            {
                infixExpression_.Add(new Operator.Binary.Mult());
            }
            infixExpression_.Add(new Operand.Variable(_expr[_index]));
        }

        private void ParseOperator(string _expr, int _index)
        {
            // '-'의 경우 뺄셈과 음수화의 두 가지 경우가 있다.
            switch (_expr[_index])
            {
                case '+': infixExpression_.Add(new Operator.Binary.Plus()); break;
                case '*': infixExpression_.Add(new Operator.Binary.Mult()); break;
                case '/': infixExpression_.Add(new Operator.Binary.Div()); break;
                case '-':
                    if (_index == 0      // 가장 앞인 경우(예: '-'x + y)
                        || _index > 0    // 바로 앞이 또 연산자이거나 왼쪽괄호인 경우(예: x'*-'y , x*'(-'y + z))
                        && (CalcForm.CheckValueType(_expr[_index - 1], CalcForm.ValueType.OPERATOR)
                            || CalcForm.CheckValueType(_expr[_index - 1], CalcForm.ValueType.OPERATOR)))
                        infixExpression_.Add(new Operator.Unary.Negation());
                    else
                        infixExpression_.Add(new Operator.Binary.Minus());
                    break;
            }
        }

        private void ParseBracket(string _expr, int _index)
        {
            switch (_expr[_index])
            {
                case '(':
                    if (_index > 0   // 바로 앞이 숫자/변수인 경우 곱셈연산이 생략된 것이므로 추가해준다.
                        && (CalcForm.CheckValueType(_expr[_index - 1], CalcForm.ValueType.NUMERIC)
                            || CalcForm.CheckValueType(_expr[_index - 1], CalcForm.ValueType.VARIABLE)))
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



        private List<object>            infixExpression_    = new List<object>();
        private ExpressionTree          exprTree_           = new ExpressionTree();
        private ShuntingYardAlgorithm   sya_                = new ShuntingYardAlgorithm();
    }
}
