using System.Collections.Generic;

namespace Octopus.Tree
{
    public class Tree<T>
    {
        public TreeNode<T> Root { get; }
        public int Depth { get; private set; }
        public int TotalNodes { get; private set; }
        public Walker<T> Walker { get; }

        public Tree(T data)
        {
            Walker = new Walker<T>(this);
            Root = new TreeNode<T> { Data = data, IsRoot = true };
            TotalNodes = 1;
            Depth = 0;
        }

        public void AddNode(TreeNode<T> newNode, TreeNode<T> parentNode)
        {
            if (parentNode.Childrens == null)
            {
                parentNode.InitChildren();
            }

            parentNode.Childrens.Add(newNode);
            newNode.SetParent(parentNode);
            ++TotalNodes;
            newNode.Level = parentNode.Level + 1;

            if (GetChildrenAtLevel(newNode.Level, Root).Count == 1)
            {
                Depth++;
            }
        }

        public void AddNode(T data)
        {
            var newNode = new TreeNode<T> { Data = data };
            AddNode(newNode, Root);
        }

        public void AddNode(T data, int levelIndex, int attachToNodeIndex)
        {
            var prevLevelChildrens = GetChildrenAtLevel(levelIndex, Root);
            if (prevLevelChildrens != null)
            {
                var root = prevLevelChildrens[attachToNodeIndex];
                AddNode(data, root);
            }
            else
            {
                AddNode(data, Root);
            }

        }

        public TreeNode<T> AddNode(T data, TreeNode<T> parentNode)
        {
            var newNode = new TreeNode<T> { Data = data };
            AddNode(newNode, parentNode);
            return newNode;
        }

        public List<TreeNode<T>> GetChildrens(TreeNode<T> node)
        {
            return node.Childrens;
        }

        public List<TreeNode<T>> GetChildrenAtLevel(int level, TreeNode<T> root)
        {
            List<TreeNode<T>> list = new List<TreeNode<T>>();
            if (level == 0)
            {
                //there is no children at level 0, only root is present.
                return null;
            }

            if (level == 1)
            {
                return root.Childrens;
            }

            var childrenList = GetChildrenAtLevel(level - 1, root);

            foreach (var treeNode in childrenList)
            {
                if (treeNode.Childrens != null)
                {
                    list.AddRange(treeNode.Childrens);
                }
            }

            return list;
        }

        public List<TreeNode<T>> GetSiblings(TreeNode<T> node)
        {
            return node.Parent?.Childrens;
        }

    }
}
