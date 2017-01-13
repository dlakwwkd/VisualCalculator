using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Operator.Binary
{
    class Plus : IBinaryOper
    {
        public string   Name { get; } = "+";

        public bool     IsLeftAssociative() => true;
        public int      GetPrecedence()     => 2;

        public double   Calc(double _left, double _right) => _left + _right;
    }
}
