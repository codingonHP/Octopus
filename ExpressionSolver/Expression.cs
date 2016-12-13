namespace ExpressionSolver
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
}