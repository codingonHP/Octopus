﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Octopus.Attributes;

namespace Octopus.Container
{
    public class ContainerManager
    {
        private static readonly object Lock = new object();
        private ContainerManager()
        {

        }
        private readonly List<Type> _injectableTypes = new List<Type>();
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
            var foundInjectables = Finder.FindAllInjectables(calledFromAssembly);
            AddToInjectableTypesCollection(foundInjectables);
        }

        internal void ActivateTypes(object @this, Type declaringType)
        {
            var properties = declaringType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                          .Where(p => p.GetCustomAttribute<InjectAttribute>() != null &&
                                                        (p.PropertyType.IsInterface || p.PropertyType.IsAbstract ||
                                                        (p.PropertyType.IsGenericType && p.PropertyType.Namespace == "System.Collections.Generic" && p.PropertyType.GenericTypeArguments.Length == 1)));

            foreach (var propertyInfo in properties)
            {
                switch (propertyInfo.PropertyType.GeTokenType())
                {
                    case TokenType.List:
                        ActivateCollection(@this, propertyInfo);
                        break;

                    default:
                        {
                            var type = GetActivationType(propertyInfo);
                            var instanceValue = Activator.CreateInstance(type);
                            propertyInfo.SetValue(@this, instanceValue);

                            break;
                        }
                }
            }
        }

        internal Type GetActivationType(PropertyInfo propertyInfo)
        {
            var type = _injectableTypes.FirstOrDefault(i => i.GetInterface(propertyInfo.PropertyType.Name) != null);
            return type;
        }

        internal void ActivateCollection(object @this, PropertyInfo propertyInfo)
        {
          
            Type genericArg = propertyInfo.PropertyType.GenericTypeArguments[0];
            List<Type> typeList = _injectableTypes.Where(i => i.GetInterface(genericArg.Name) != null).ToList();

            var collectionInstance = Activator.CreateInstance(propertyInfo.PropertyType);

            foreach (var type in typeList)
            {
                var instanceValue = Activator.CreateInstance(type);
                var addMethod = collectionInstance.GetType().GetMethod("Add");
                addMethod.Invoke(collectionInstance, new[] { instanceValue });
            }

            propertyInfo.SetValue(@this, collectionInstance);
          
        }

        internal void AddToInjectableTypesCollection(List<Type> foundInjectables)
        {
            foreach (var foundInjectable in foundInjectables)
            {
                if (_injectableTypes.FirstOrDefault(t => t.FullName == foundInjectable.FullName) == null)
                {
                    _injectableTypes.Add(foundInjectable);
                }
            }
        }

    }
}