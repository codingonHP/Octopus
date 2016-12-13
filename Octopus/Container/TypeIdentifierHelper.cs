using System;

namespace Octopus.Container
{
    public enum TokenType
    {
        List,
        Dictionary,
        Set,
        Event,
        Delegate,
        Class,
        Interface
    }


    public static class TypeIdentifierHelper
    {
        public static TokenType GeTokenType(this Type type)
        {
            if (type.Namespace == "System.Collections.Generic")
            {
                return TokenType.List;
            }

            return TokenType.Class;
        }
    }
}
