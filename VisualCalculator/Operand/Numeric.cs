using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Operand
{
    class Numeric : IOperand
    {
        public Numeric(double _value)
        {
            value_ = _value;
        }

        public string Name { get { return value_.ToString(); } }
        public double Value
        {
            get { return value_; }
        }

        private double value_;
    }
}
