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

        }

        public void SetStringFromResult(double _result)
        {

        }



        private void ParseInfixExprFromString(string _expression)
        {
            for (int i = 0; i < _expression.Length; ++i)
            {
                char curChar = _expression[i];
                if (CalcForm.CheckValueType(curChar, CalcForm.ValueType.NUMERIC))
                {
                    for (int j = i + 1; j < _expression.Length; ++j)
                    {
                        if (CalcForm.CheckValueType(_expression[j], CalcForm.ValueType.NUMERIC)
                            || CalcForm.CheckValueType(_expression[j], CalcForm.ValueType.DECIMAL))
                            continue;

                        var temp = _expression.Substring(i, j - i);
                        var value = new Operand.Numeric(temp);
                        infixExpression_.Add(value);
                        i = j - 1;
                        break;
                    }
                }
                else if (CalcForm.CheckValueType(curChar, CalcForm.ValueType.VARIABLE))
                {
                    if (i > 0 && CalcForm.CheckValueType(_expression[i - 1], CalcForm.ValueType.NUMERIC))
                    {
                        infixExpression_.Add(new Operator.Binary.Mult());
                    }
                    infixExpression_.Add(new Operand.Variable(curChar));
                }
                else if (CalcForm.CheckValueType(curChar, CalcForm.ValueType.OPERATOR))
                {
                    switch (curChar)
                    {
                        case '+': infixExpression_.Add(new Operator.Binary.Plus()); break;
                        case '*': infixExpression_.Add(new Operator.Binary.Mult()); break;
                        case '/': infixExpression_.Add(new Operator.Binary.Div()); break;
                        case '-':
                            if (i == 0
                                || i > 0
                                && CalcForm.CheckValueType(_expression[i - 1], CalcForm.ValueType.OPERATOR))
                                infixExpression_.Add(new Operator.Unary.Negation());
                            else
                                infixExpression_.Add(new Operator.Binary.Minus());
                            break;
                    }
                }
                else
                {
                    switch (curChar)
                    {
                        case '(':
                            if (i > 0
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
