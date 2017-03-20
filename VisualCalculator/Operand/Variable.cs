namespace VisualCalculator.Operand
{
    class Variable : IOperand
    {
        public Variable(char name)
        {
            Name = name.ToString();
        }

        public double Value { get; set; } = 0.0;
        public string Name  { get; }
    }
}
