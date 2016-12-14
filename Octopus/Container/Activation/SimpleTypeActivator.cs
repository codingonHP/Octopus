using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Octopus.Container.Activation
{
    public class SimpleTypeActivator : Activate, IActivate
    {
        public void Activate(object @this, PropertyInfo propertyInfo, List<Type> injectablesAvailable)
        {
            var type = injectablesAvailable.FirstOrDefault(i => i.GetInterface(propertyInfo.PropertyType.Name) != null);
            if (type != null)
            {
                var instanceValue = Activator.CreateInstance(type);
                propertyInfo.SetValue(@this, instanceValue);
            }
        }
    }
}