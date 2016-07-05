using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Operator.Binary
{
    class Minus : IBinaryOper
    {
        public double Calc(double _left, double _right)
        {
            return _left - _right;
        }
    }
}
