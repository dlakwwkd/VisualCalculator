namespace VisualCalculator.Operand
{
    class Numeric : IOperand
    {
        public Numeric(double value)
        {
            Value = value;
        }

        public double Value { get; }
        public string Name => Value.ToString();
    }
}
