using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Octopus.Tree;

namespace Octopus.TypeWalker
{
    public class TypeTreeConstructor
    {
        private readonly Tree<Type> _typeTree;
        private readonly Type _type;
        public event Action<object, Tree<Type>> OnTreeConstructed;

        public TypeTreeConstructor(Type type)
        {
            _typeTree = new Tree<Type>(type);
            _type = type;
        }

        public Tree<Type> ConstructTypeTree()
        {
            ConstructTree(_type, _typeTree.Root);
            return _typeTree;
        }

        private void ConstructTree(Type type, TreeNode<Type> parentNode)
        {
            Dictionary<Type, TreeNode<Type>> catalog = new Dictionary<Type, TreeNode<Type>>();
            if (type.IsGenericType)
            {
                var genericParameter = type.GetGenericArguments();
                foreach (var gType in genericParameter)
                {
                    var insertedNode = _typeTree.AddNode(gType, parentNode);
                    catalog.Add(gType, insertedNode);
                }

                OnOnNodeReached(this, _typeTree);

                foreach (var gType in genericParameter)
                {
                    catalog.TryGetValue(gType, out parentNode);
                    ConstructTree(gType, parentNode);
                }
            }
        }

        public void PrintTree()
        {
            Debug.WriteLine("New tree");
            if (_typeTree.Root != null)
            {
                PrintTree(_typeTree.Root);
            }
        }

        private void PrintTree(TreeNode<Type> node)
        {
            Debug.WriteLine(node.Data.Name);
            if (node.Childrens != null)
            {
                foreach (var nodeChildren in node.Childrens)
                {
                    PrintTree(nodeChildren);
                }
            }
        }

        protected virtual void OnOnNodeReached(object sender, Tree<Type> tree)
        {
            OnTreeConstructed?.Invoke(this, tree);
        }
    }
}
