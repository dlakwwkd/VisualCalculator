namespace VisualCalculator.Operator.Binary
{
    class Mult : IBinaryOper
    {
        public string   Name { get; } = "*";

        public double   Calc(double left, double right)
            => left * right;

        public bool     IsLeftAssociative() => true;
        public int      GetPrecedence()     => 3;
    }
}
