namespace ExpressionSolver
{
    public interface IExpressionParser
    {
        Expression ParseTextToExpression(string expression);
    }
}