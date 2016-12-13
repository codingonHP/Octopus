using System.Reflection;

namespace Octopus.Container.Interfaces
{
    public interface IContainer
    {
        void Init(object helloWorld);
        void Init(object @this, params Assembly[] assemblies);
        void Init(object @this, string location);
    }
}