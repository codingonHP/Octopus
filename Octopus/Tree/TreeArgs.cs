using System;

namespace Octopus.Tree
{
    public class TreeArgs<T> : EventArgs
    {
        public TreeNode<T> TreeNode { get; set; }
    }
}