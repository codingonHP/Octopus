using System;
using Octopus.Tree;

namespace Octopus.TypeWalker
{
    public class TypeTreeConstructor
    {
        private readonly Tree<Type> _typeTree;
        public TypeTreeConstructor(Type type)
        {
            _typeTree = new Tree<Type>(type);
        }

        public Tree<Type> ConstructTypeTree(Type type)
        {
            ConstructTree(type, 0);
            return _typeTree;
        }

        private void ConstructTree(Type type, int levelIndex)
        {
            if (type.IsGenericType)
            {
                var genericParameter = type.GetGenericArguments();
                foreach (var gType in genericParameter)
                {
                    _typeTree.AddNode(gType, levelIndex, levelIndex++);
                }

                foreach (var gType in genericParameter)
                {
                    ConstructTree(gType, ++levelIndex);
                }
            }
        }
    }
}
