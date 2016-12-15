using System.Collections.Generic;

namespace Octopus.Tree
{
    public class TreeNode<T>
    {
        public T Data { get; set; }
        public TreeNode<T> Parent { get; private set; }
        public List<TreeNode<T>> Childrens { get; private set; }
        public bool IsRoot { get; set; }
        public int Level { get; set; }

        public void InitChildren()
        {
            Childrens = new List<TreeNode<T>>();
        }

        public void InitParent()
        {
            Parent = new TreeNode<T>();
        }

        public void SetParent(TreeNode<T> parent)
        {
            Parent = parent;
        }
    }
}