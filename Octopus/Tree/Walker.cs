using System.Collections.Generic;
using System.Linq;

namespace Octopus.Tree
{
    public enum WalkType
    {
        Dfs,
        Bfs,
        TopDown
    }

    public class Walker<T>
    {
        private readonly Tree<T> _tree;

        public Walker(Tree<T> tree)
        {
            _tree = tree;
        }

        public virtual IEnumerable<TreeNode<T>> Traverse(WalkType walkType)
        {
            switch (walkType)
            {
                case WalkType.Dfs:
                    return Dfs(_tree.Root);
                case WalkType.Bfs:
                    return Bfs(_tree);
                case WalkType.TopDown:
                    return TopDown(_tree.Root);
            }

            return null;
        }

        private IEnumerable<TreeNode<T>> TopDown(TreeNode<T> node)
        {
            yield return node;

            if (node.Childrens != null)
            {
                var childrens = node.Childrens;

                foreach (var children in childrens)
                {
                    foreach (var treeNode in TopDown(children))
                    {
                        yield return treeNode;
                    }
                }
            }
        }

        private IEnumerable<TreeNode<T>> Bfs(Tree<T> tree)
        {
            yield return tree.Root;

            for (int i = 1; i <= tree.Depth; i++)
            {
                var enumerable = tree.GetChildrenAtLevel(i, tree.Root)?.AsEnumerable();
                if (enumerable != null)
                {
                    foreach (var treeNode in enumerable)
                    {
                        yield return treeNode;
                    }
                }
            }
        }

        private IEnumerable<TreeNode<T>> Dfs(TreeNode<T> node)
        {
            if (node.Childrens != null)
            {
                foreach (var nodeChildren in node.Childrens)
                {
                    var list = Dfs(nodeChildren);
                    foreach (var treeNode in list)
                    {
                        yield return treeNode;
                    }
                }
            }

            yield return node;
        }
    }
}
