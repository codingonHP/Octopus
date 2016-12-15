using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Octopus.Attributes;
using Octopus.Container.Activation;

namespace Octopus.Container
{
    public class ContainerManager
    {
        private static readonly object Lock = new object();
        private ContainerManager()
        {

        }
        public readonly List<Type> InjectableTypes = new List<Type>();
        private readonly List<string> _alreadyScannedAssembly = new List<string>();
        private static ContainerManager _instance;

        public static ContainerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        _instance = new ContainerManager();
                    }
                }

                return _instance;
            }
        }

        internal void ScanAssembly(Assembly calledFromAssembly)
        {
            if (!_alreadyScannedAssembly.Contains(calledFromAssembly.FullName))
            {
                var foundInjectables = Finder.FindAllInjectables(calledFromAssembly);
                AddToInjectableTypesCollection(foundInjectables);
                _alreadyScannedAssembly.Add(calledFromAssembly.FullName);
            }

        }

        internal void ActivateTypes(object @this, Type declaringType)
        {
            ActivatorFactory activatorFactory = new ActivatorFactory();

            var properties = declaringType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                          .Where(p => p.GetCustomAttribute<InjectAttribute>() != null);

            foreach (var propertyInfo in properties)
            {
                var act = activatorFactory.GetActivatorForType(propertyInfo.PropertyType);
                act.Activate(@this, propertyInfo, InjectableTypes);
            }
        }

        internal void AddToInjectableTypesCollection(List<Type> foundInjectables)
        {
            foreach (var foundInjectable in foundInjectables)
            {
                if (InjectableTypes.FirstOrDefault(t => t.FullName == foundInjectable.FullName) == null)
                {
                    InjectableTypes.Add(foundInjectable);
                }
            }
        }

    }
}