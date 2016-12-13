using Octopus.Container.Interfaces;

namespace Octopus.Container
{
    public class ContainerFactory : IContainerFactory
    {
        public static Container Container = Container.Instance;

        public Container GetContainer()
        {
            return Container;
        }

        
    }
}