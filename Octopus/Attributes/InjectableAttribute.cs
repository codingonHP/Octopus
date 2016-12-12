using System;

namespace Octopus.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectableAttribute : Attribute
    {
        private readonly Type _type;

        public InjectableAttribute()
        {
            
        }

        public InjectableAttribute(Type type)
        {
            _type = type;
        }

    }
}
