using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Operand
{
    class Numeric : IOperand
    {
        public Numeric(string _value)
        {
            value_ = double.Parse(_value);
        }


        private double value_;
    }
}
