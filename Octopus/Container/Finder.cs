using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Octopus.Attributes;

namespace Octopus.Container
{
    public static class Finder
    {
        public static List<Type> FindAllInjectables(Assembly assembly)
        {
            List<Type> injectables = new List<Type>(); // impl, interface
            if (assembly == null)
            {
                return null;
            }

            var injectableList = assembly.DefinedTypes.Where(dt => dt.IsClass && dt.GetCustomAttribute<InjectableAttribute>() != null);

            foreach (var typeInfo in injectableList)
            {
                if (!injectables.Contains(typeInfo))
                {
                    injectables.Add(typeInfo);
                }
            }

            return injectables;
        }
    }
}
