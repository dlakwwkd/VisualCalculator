namespace VisualCalculator.Operator.Unary
{
    class Negation : IUnaryOper
    {
        public string Name { get; } = "-";

        public double Calc(double source)
            => -source;
    }
}
