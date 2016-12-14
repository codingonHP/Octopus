using System;

namespace Octopus.Container.Activation
{
    public class Activate
    {
        public object Initialize(Type type)
        {
           return Activator.CreateInstance(type);
        }
    }
}