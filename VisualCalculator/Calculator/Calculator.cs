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
            ParseInfixExprFromString(_expression);
        }

        public void SetStringFromResult(double _result)
        {

        }

        private bool ParseInfixExprFromString(string _expression)
        {



            return true;
        }

        private List<object>            infixExpression_    = new List<object>();
        private ExpressionTree          exprTree_           = new ExpressionTree();
        private ShuntingYardAlgorithm   sya_                = new ShuntingYardAlgorithm();
    }
}
