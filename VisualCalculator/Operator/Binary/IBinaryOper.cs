using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Operator.Binary
{
    interface IBinaryOper : IOperator
    {
        bool    IsLeftAssociative();
        int     GetPrecedence();

        double  Calc(double _left, double _right);
    }
}
