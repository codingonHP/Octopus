using System;
using System.Collections.Generic;
using System.Reflection;
using Octopus.Attributes;
using Octopus.Container;

namespace ExpressionSolver
{
    public class ExpressionSolver
    {
        public string DefaultExtensionPath { get; set; }
        public string Expression { get; }

        [Inject]
        public List<IExpressionCompute> Solvers { get; set; }

        [Inject]
        public IExpressionParser ExpressionParser { get; set; }

        public ExpressionSolver(string expressionText)
        {
            if (string.IsNullOrEmpty(DefaultExtensionPath))
            {
                DefaultExtensionPath = @"C:\Users\ANANDV4\Documents\Visual Studio 2015\Projects\Octopus\Octopus Unit Test\Extensions";
            }
            Expression = expressionText;

            Container container = Container.Instance;
            container.Init(this,new []{ DefaultExtensionPath });
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
}