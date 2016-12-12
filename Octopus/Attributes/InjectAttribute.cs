using System;

namespace Octopus.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
        public Type Using { get; set; }
    }
}