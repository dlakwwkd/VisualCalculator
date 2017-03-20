namespace VisualCalculator.Operator.Binary
{
    interface IBinaryOper : IOperator
    {
        double  Calc(double left, double right);

        bool    IsLeftAssociative();
        int     GetPrecedence();
    }
}
