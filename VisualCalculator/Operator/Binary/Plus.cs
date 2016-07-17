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

        public bool     IsLeftAssociative() { return true; }
        public int      GetPrecedence() { return 2; }

        public double   Calc(double _left, double _right) 
        {
            return _left + _right;
        }
    }
}
