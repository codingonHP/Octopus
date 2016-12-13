using System;
using ExpressionSolver;
using Octopus.Attributes;

namespace ExpressionSolverExtension
{
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
