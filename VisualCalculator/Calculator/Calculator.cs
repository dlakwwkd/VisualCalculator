using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Calculator
{
    class Calculator
    {
        public bool ParseInfixExprFromString(string _expression)
        {
            return true;
        }

        public void SetStringFromResult(double _result)
        {

        }

        public void Calculate()
        {

        }

        private List<object>            infixExpression_    = new List<object>();
        private ExpressionTree          exprTree_           = new ExpressionTree();
        private ShuntingYardAlgorithm   sya_                = new ShuntingYardAlgorithm();
    }
}
