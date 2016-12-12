using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octopus.Attributes;
using Octopus.Container;

namespace Octopus_Unit_Test
{
    public class HelloWorld
    {
        [Inject]
        private IHello Hello { get; set; }

        public HelloWorld()
        {
            Container container = ContainerFactory.Container;
            container.Init(this);
        }

        public string SayHello(string message)
        {
          return Hello.SayHello(message);
        }
    }

    public interface IHello
    {
        string SayHello(string message);
    }

    [Injectable]
    public class Hello : IHello
    {
        public string SayHello(string message)
        {
            return "Say hello : " + message;
        }
    }


    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            HelloWorld helloWorld = new HelloWorld();
            var sayResult = helloWorld.SayHello("hello sam");
            Assert.AreEqual("Say hello : hello sam", sayResult);
        }
    }
}
