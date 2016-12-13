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

    public class User
    {
        [Inject]
        public ILogin Login { get; set; }

        [Inject]
        private IHello Hello { get; set; }


        public User()
        {
            Container container = ContainerFactory.Container;
            container.Init(this);
        }
    }
}