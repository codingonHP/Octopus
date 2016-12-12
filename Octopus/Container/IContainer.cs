using System.Reflection;

namespace Octopus.Container
{
    public interface IContainer
    {
        void Init(object helloWorld);
        void Init(object @this, params Assembly[] assemblies);
        void Init(object @this, string location);
    }
}