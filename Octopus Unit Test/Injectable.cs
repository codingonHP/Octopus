using System;
using Octopus.Attributes;

namespace Octopus_Unit_Test
{
    [Injectable]
    public class Hello : IHello
    {
        public string SayHello(string message)
        {
            return "Say hello : " + message;
        }
    }

    [Injectable]
    public class LoginValidator : ILogin
    {
        public bool ValidateLogin(string userName)
        {
            return true;
        }
    }

    [Injectable]
    public class ExpressionParser : IExpressionParser
    {
        public Expression ParseTextToExpression(string expression)
        {
            var tokenArry = expression.ToCharArray();
            Expression exp = new Expression(expression)
            {
                LeftOperand = new LeftOperand { Value = tokenArry[0] },
                Operator = new Operator { Symbol = tokenArry[1] },
                RightOperand = new RightOperand { Value = tokenArry[2] }
            };

            return exp;
        }
    }

    [Injectable]
    public class BinaryAddExpressionSolver : IExpressionCompute
    {
        public Result ComputeResult(Expression expression)
        {
            if (expression.Operator.Symbol == '+')
            {
                return new Result
                {
                    Computed = true,
                    ComputedType = expression.LeftOperand.Value.GetType(),
                    Output = int.Parse(expression.LeftOperand.Value.ToString()) + int.Parse(expression.RightOperand.Value.ToString())
                };
            }

            return new Result
            {
                Exception = new Exception("Symbol not recognized")
            };
        }
    }

    [Injectable]
    public class BinarySubstractExpressionSolver : IExpressionCompute
    {
        public Result ComputeResult(Expression expression)
        {
            if (expression.Operator.Symbol == '-')
            {
                return new Result
                {
                    Computed = true,
                    ComputedType = expression.LeftOperand.Value.GetType(),
                    Output = int.Parse(expression.LeftOperand.Value.ToString()) - int.Parse(expression.RightOperand.Value.ToString())
                };
            }

            return new Result
            {
                Exception = new Exception("Symbol not recognized")
            };
        }
    }

    [Injectable]
    public class BinaryMultiplySolver : IExpressionCompute
    {
        public Result ComputeResult(Expression expression)
        {
            if (expression.Operator.Symbol == '*')
            {
                return new Result
                {
                    Computed = true,
                    ComputedType = expression.LeftOperand.Value.GetType(),
                    Output = int.Parse(expression.LeftOperand.Value.ToString()) * int.Parse(expression.RightOperand.Value.ToString())
                };
            }

            return new Result
            {
                Exception = new Exception("Symbol not recognized")
            };
        }
    }
}