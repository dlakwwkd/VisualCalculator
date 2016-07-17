using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCalculator.Operand
{
    class Variable : IOperand
    {
        public Variable(char _value)
        {
            value_ = _value;
        }

        public string Name { get { return value_.ToString(); } }

        private char value_;
    }
}
