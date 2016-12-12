using System.Reflection;

namespace Octopus.Container
{
    public class Container : IContainer
    {
        readonly ContainerManager _containerManager = ContainerManager.Instance;

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

        public static object Lock = new object();
        private static Container _instance;

        private Container()
        {

        }

        public void Init(object @this)
        {
            var calledFromAssembly = @this.GetType().Module.Assembly;
            var declaringType = @this.GetType();

            _containerManager.ScanAssembly(calledFromAssembly);
            _containerManager.ActivateTypes(@this, declaringType);
        }

        public void Init(object @this, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                Init(@this, assembly);
            }
        }

        public void Init(object @this, string location)
        {
            var assembly = Assembly.LoadFile(location);
            Init(@this, assembly);
        }
    }
}
