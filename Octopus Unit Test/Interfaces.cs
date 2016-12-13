using System;

namespace Octopus_Unit_Test
{
    public interface IHello
    {
        string SayHello(string message);
    }

    public interface ILogin
    {
        bool ValidateLogin(string userName);
    }

    public interface IOperation
    {
        Expression OperationResult(Expression expression);
    }

    public interface IExpressionParser
    {
        Expression ParseTextToExpression(string expression);
    }


    public interface IExpressionCompute
    {
        Result ComputeResult(Expression expression);
    }

}