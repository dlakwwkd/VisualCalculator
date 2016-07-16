using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Calculator
{
    class ShuntingYardAlgorithm
    {
        public async Task Run(List<object> _infixExpr)
        {
            await Temp();
        }


        private Task Temp()
        {
            return Task.Factory.StartNew(() =>
            {

            });
        }

        private List<object> postfixExpression_ = new List<object>();
    }
}
