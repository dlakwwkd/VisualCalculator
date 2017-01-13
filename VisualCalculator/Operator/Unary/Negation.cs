using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Operator.Unary
{
    class Negation : IUnaryOper
    {
        public string Name { get; } = "-";

        public double Calc(double _source) => -_source;
    }
}
