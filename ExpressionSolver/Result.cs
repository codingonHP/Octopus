using System;

namespace ExpressionSolver
{
    public class Result
    {
        public bool Computed { get; set; }
        public object Output { get; set; }
        public Type ComputedType { get; set; }
        public Exception Exception { get; set; }

    }
}