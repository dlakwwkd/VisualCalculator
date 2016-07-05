using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Operator.Unary
{
    class Negation : IUnaryOper
    {
        public double Calc(double _source)
        {
            return -_source;
        }
    }
}
