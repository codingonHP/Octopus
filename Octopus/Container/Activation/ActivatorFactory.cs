using System;
using System.Linq;

namespace Octopus.Container.Activation
{
    public class ActivatorFactory
    {
        public IActivate GetActivatorForType(Type type)
        {
            var interfacesList = type.GetInterfaces();

            if (interfacesList.Any(t => t.FullName == "System.Collections.ICollection"))
            {
                var addMethodPresent = type.GetMethod("Add");
                if (addMethodPresent != null)
                {
                    var parameterCount = addMethodPresent.GetParameters().Length;
                    if (parameterCount == 1)
                    {
                        return new ListActivator();
                    }

                    if (parameterCount == 2)
                    {
                        return new DictionaryActivator();
                    }
                }
            }

            return new SimpleTypeActivator();

        }

    }


}
