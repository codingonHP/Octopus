using System.IO;
using System.Reflection;
using Octopus.Container.Interfaces;

namespace Octopus.Container
{
    public class Container : IContainer
    {
        private static readonly object Lock = new object();
        private static Container _instance;
        private Container() { }

        public readonly ContainerManager ContainerManager = ContainerManager.Instance;

        public static Container Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        _instance = new Container();
                    }
                }

                return _instance;
            }
        }
       
        public void ScanAssemblyList(object @this, string[] locations, params Assembly[] assemblies)
        {
            Assembly calledFromAssembly = @this.GetType().Module.Assembly;
            ContainerManager.ScanAssembly(calledFromAssembly);

            foreach (var location in locations)
            {
                if (!string.IsNullOrEmpty(location))
                {
                    var files = Directory.GetFiles(location);

                    foreach (var filePath in files)
                    {
                        if (Path.GetExtension(filePath) == ".dll")
                        {
                            var assembly = Assembly.LoadFile(filePath);
                            ContainerManager.ScanAssembly(assembly);
                        }
                    }
                }
            }

            foreach (var assembly in assemblies)
            {
                ContainerManager.ScanAssembly(assembly);
            }
        }

        public void Init(object @this)
        {
            ScanAssemblyList(@this, new string[0]);
            ActivateType(@this);
        }

        public void Init(object @this, params Assembly[] assemblies)
        {
            ScanAssemblyList(@this, new string[0], assemblies);
            ActivateType(@this);
        }

        public void Init(object @this, string location)
        {
            ScanAssemblyList(@this, new[] { location });
            ActivateType(@this);
        }

        public void Init(object @this, string[] locations, params Assembly[] assemblies)
        {
            ScanAssemblyList(@this, locations, assemblies);
            ActivateType(@this);
        }

        private void ActivateType(object @this)
        {
            var declaringType = @this.GetType();
            ContainerManager.ActivateTypes(@this, declaringType);
        }


    }
}
