using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Operand
{
    class Variable : IOperand
    {
        public Variable(char _name)
        {
            name_ = _name;
            value_ = 0.0;
        }

        public string Name { get { return name_.ToString(); } }
        public double Value
        {
            get { return value_; }
            set { value_ = value; }
        }

        private char    name_;
        private double  value_;
    }
}
