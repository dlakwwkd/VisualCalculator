using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Operator.Unary
{
    interface IUnaryOper : IOperator
    {
        double Calc(double _source);
    }
}
