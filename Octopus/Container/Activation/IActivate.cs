using System;
using System.Collections.Generic;
using System.Reflection;

namespace Octopus.Container.Activation
{
    public interface IActivate
    {
        void Activate(object @this, PropertyInfo  propertyInfo, List<Type> injectablesAvailable );
    }
}
