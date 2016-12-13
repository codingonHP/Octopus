using System.Reflection;

namespace Octopus.Container.Interfaces
{
    public interface IContainer
    {
        void ScanAssemblyList(object @this, string[] locations, params Assembly[] assemblies);
        void Init(object @this);
        void Init(object @this, params Assembly[] assemblies);
        void Init(object @this, string location);

        void Init(object @this, string[] locations, params Assembly[] assemblies);
    }
}