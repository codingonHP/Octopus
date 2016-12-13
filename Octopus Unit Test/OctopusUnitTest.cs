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
    }
}
