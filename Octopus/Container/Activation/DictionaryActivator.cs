using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Octopus.Container.Activation
{
    public class DictionaryActivator : Activate, IActivate
    {
        public static int Key;

        public void Activate(object @this, PropertyInfo propertyInfo, List<Type> injectablesAvailable)
        {
            ActivatorFactory activatorFactory = new ActivatorFactory();

            Type keyArg = propertyInfo.PropertyType.GenericTypeArguments[0];
            Type valArg = propertyInfo.PropertyType.GenericTypeArguments[1];
            List<Type> valueList = injectablesAvailable.Where(i => i.GetInterface(valArg.Name) != null).ToList();
            var collectionInstance = Activator.CreateInstance(propertyInfo.PropertyType);

            foreach (var type in valueList)
            {
                var instanceValue = Activator.CreateInstance(type);
                var addMethod = collectionInstance.GetType().GetMethod("Add");

                if (keyArg.IsPrimitive || keyArg == typeof(string))
                {
                    addMethod.Invoke(collectionInstance, new[] { GetNextKey(keyArg), instanceValue });
                }
                else
                {
                    //TODO : Need some work here
                    //var act = activatorFactory.GetActivatorForType(keyArg);
                    //addMethod.Invoke(collectionInstance, new[] { Initialize(keyArg), instanceValue });
                }
            }

            if (@this != null)
            {
                propertyInfo.SetValue(@this, collectionInstance);
            }
        }

        private object GetNextKey(Type type)
        {
            
            if (typeof(int) == type || typeof(double) == type || typeof(float) == type)
            {
                return Key++;
            }
            if (typeof(string) == type)
            {
                return "abcdefghijklmnopqrstuvwxyz".ToCharArray()[(Key++)%26].ToString();
            }

            if (typeof(char) == type)
            {
                return "abcdefghijklmnopqrstuvwxyz".ToCharArray()[(Key++)%26];
            }

            return null;
        }
    }
}