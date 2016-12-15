using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Octopus.Attributes;

namespace Octopus.Container.Activation
{



    [Inject]
    class Person<T>
    {



        [Inject]
        public List<List<List<List<List<List<IPerson>>>>>> MegaListOfPerson { get; set; }

        [Inject]
        public List<List<IPerson>> ListInList { get; set; }

        public Person()
        {
            Container container = Container.Instance;
            container.ScanAssemblyList(this, new[] { string.Empty });
            var scanList = container.ContainerManager.InjectableTypes;

            MegaListOfPerson = new List<List<List<List<List<List<IPerson>>>>>>
            {
                new List<List<List<List<List<IPerson>>>>>
                {
                    new List<List<List<List<IPerson>>>>
                    {
                        new List<List<List<IPerson>>>
                        {
                            new List<List<IPerson>>
                            {
                                new List<IPerson>
                                {
                                    new HelloPerson()
                                }
                            }
                        }
                    }
                }
            };
        }
    }

    internal interface IPerson
    {

    }

    [Injectable]
    public class HelloPerson : IPerson
    {

    }

    public class Activate
    {
        public Dictionary<Type, object> Mapper = new Dictionary<Type, object>();

        public void Init(object @this, PropertyInfo property, List<Type> availableInjectors)
        {
            var type = property.PropertyType;
            var instance = Initialize(@this, type, false, availableInjectors);

            if (instance != null)
            {
                property.SetValue(@this, instance);
            }

        }

        public object Initialize(object @this, Type type, bool generic, List<Type> availableInjectors)
        {
            if (type.IsGenericType)
            {
                var genericArgs = type.GetGenericArguments();
                foreach (var genericArg in genericArgs)
                {
                    if (genericArg.IsInterface || genericArg.IsAbstract)
                    {
                        var addMethodPresent = genericArg.GetMethod("Add");
                        if (addMethodPresent != null)
                        {
                            var parameters = addMethodPresent.GetParameters();
                            foreach (var parameter in parameters)
                            {
                                if (parameter.ParameterType.IsGenericType)
                                {
                                    var instance = Initialize(@this, parameter.ParameterType, true, availableInjectors);
                                }
                            }
                        }
                        else
                        {
                            var instance = Initialize(@this, genericArg, true, availableInjectors);
                        }
                    }
                }
            }


            if ((type.IsInterface || type.IsAbstract) && generic)
            {
                var inject = availableInjectors.FirstOrDefault(i => i.GetInterface(type.FullName) != null);
                if (inject != null)
                {
                    var instance = Activator.CreateInstance(inject);
                    Mapper.Add(type, instance);
                    return null;
                }
            }
            else if (type.IsInterface || type.IsAbstract)
            {
                var inject = availableInjectors.FirstOrDefault(i => i.GetInterface(type.FullName) != null);
                if (inject != null)
                {
                    var instance = Activator.CreateInstance(inject);
                    return instance;
                }
            }

            return null;

        }
    }
}