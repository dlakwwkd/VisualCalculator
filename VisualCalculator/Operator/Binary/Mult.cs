using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Operator.Binary
{
    class Mult : IBinaryOper
    {
        public string   Name { get; } = "*";

        public bool     IsLeftAssociative() => true;
        public int      GetPrecedence()     => 3;

        public double   Calc(double _left, double _right) => _left * _right;
    }
}
