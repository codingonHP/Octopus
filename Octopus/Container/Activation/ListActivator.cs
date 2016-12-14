using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Octopus.Container.Activation
{
    public class ListActivator : Activate, IActivate
    {
        public void Activate(object @this, PropertyInfo propertyInfo, List<Type> injectablesAvailable)
        {
            Type genericArg = propertyInfo.PropertyType.GenericTypeArguments[0];
            List<Type> typeList = injectablesAvailable.Where(i => i.GetInterface(genericArg.Name) != null).ToList();

            var collectionInstance = Activator.CreateInstance(propertyInfo.PropertyType);

            foreach (var type in typeList)
            {
                var instanceValue = Activator.CreateInstance(type);
                var addMethod = collectionInstance.GetType().GetMethod("Add");
                addMethod.Invoke(collectionInstance, new[] { instanceValue });
            }

            propertyInfo.SetValue(@this, collectionInstance);
        }

    }
}