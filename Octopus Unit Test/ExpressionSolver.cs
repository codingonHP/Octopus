using System;
using System.Collections.Generic;
using Octopus.Attributes;
using Octopus.Container;

namespace Octopus_Unit_Test
{
    public class Expression
    {
        public Expression(string expressionText)
        {
            ExpressionText = expressionText;
        }

        public string ExpressionText { get; }

        public LeftOperand LeftOperand { get; set; }
        public RightOperand RightOperand { get; set; }
        public Operator Operator { get; set; }
    }

    public class ExpressionSolver
    {
        public string Expression { get; }

        [Inject]
        public List<IExpressionCompute> Solvers { get; set; }

        [Inject]
        public IExpressionParser ExpressionParser { get; set; }

        public ExpressionSolver(string expressionText)
        {
            Expression = expressionText;

            Container container = Container.Instance;
            container.Init(this);
        }

        public Result Solve()
        {
            var expression = ParseTextToExpression(Expression);
            var result = ComputeResult(expression);

            return result;
        }

        private Result ComputeResult(Expression expression)
        {
            Result result = new Result { Exception = new Exception("Solver not available for this type of expression") };

            foreach (var solver in Solvers)
            {
                result = solver.ComputeResult(expression);
                if (result.Computed)
                {
                    break;
                }
            }

            return result;
        }
        private Expression ParseTextToExpression(string expression)
        {
            return ExpressionParser.ParseTextToExpression(expression);
        }
    }


    public class Result
    {
        public bool Computed { get; set; }
        public object Output { get; set; }
        public Type ComputedType { get; set; }
        public Exception Exception { get; set; }

    }

    public class LeftOperand
    {
        public object Value { get; set; }
    }

    public class RightOperand
    {
        public object Value { get; set; }
    }

    public class Operator
    {
        public char Symbol { get; set; }
        public string Name { get; set; }
    }
}