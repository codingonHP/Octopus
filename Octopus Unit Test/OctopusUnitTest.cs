using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Octopus_Unit_Test
{
    [TestClass]
    public class OctopusUnitTest
    {

        [TestMethod]
        public void ClassWithSingleInjectWorks()
        {
            HelloWorld helloWorld = new HelloWorld();
            var sayResult = helloWorld.SayHello("hello sam");
            Assert.AreEqual("Say hello : hello sam", sayResult);
        }

        [TestMethod]
        public void ClassWithDoubleInjectWorks()
        {
            var user = new User();
            var output = user.Login.ValidateLogin("vishal");
            Assert.AreEqual(true, output);
        }

        [TestMethod]
        public void ExpressionSolverTest()
        {
            ExpressionSolver expressionSolver = new ExpressionSolver("2+2");
            var result = expressionSolver.Solve();

            Assert.AreEqual(4, result.Output);

            expressionSolver = new ExpressionSolver("2\\2");
            result = expressionSolver.Solve();

            Assert.AreEqual(false, result.Computed);
            Assert.AreEqual("Symbol not recognized", result.Exception.Message);

            expressionSolver = new ExpressionSolver("2*8");
            result = expressionSolver.Solve();

            Assert.AreEqual(true, result.Computed);
            Assert.AreEqual(16,result.Output);
        }
    }
}
