using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octopus.Tree;

namespace Octopus_Unit_Test
{
    [TestClass]
    public class TreeUnitTest
    {
        [TestMethod]
        public void CreateTreeAndTraversalWorksAsExpected()
        {
            List<int> items = new List<int>();
            List<int> answerList = new List<int> { 21, 22, 11, 30, 12, 100 };
            int i = 0;

            Tree<int> tree = new Tree<int>(100);
            tree.AddNode(11, 0, 0);
            tree.AddNode(12, 0, 0);
            tree.AddNode(21, 1, 0);
            tree.AddNode(22, 1, 0);
            tree.AddNode(30, 1, 1);


            tree.LeafReached += (o, node) =>
            {
                items.Add(node.TreeNode.Data);
            };

            tree.TraverseDfs();

            foreach (var item in items)
            {
                Assert.AreEqual(answerList[i++], item);
            }

            Assert.AreEqual(2, tree.Depth);
        }

        [TestMethod]
        public void GetChildrensReturnsAllTheChildrenAtLevelByLevelId()
        {
            Tree<int> tree = new Tree<int>(100);
            tree.AddNode(10, 0, 0);
            tree.AddNode(11, 0, 0);
            tree.AddNode(12, 0, 0);
            tree.AddNode(13, 0, 0);
            tree.AddNode(14, 0, 0);
            tree.AddNode(21, 1, 0);
            tree.AddNode(22, 1, 0);
            tree.AddNode(30, 1, 2);
            tree.AddNode(31, 1, 2);
            tree.AddNode(32, 1, 2);
            tree.AddNode(35, 1, 3);
            tree.AddNode(36, 1, 3);
            tree.AddNode(37, 1, 3);
            tree.AddNode(40, 2, 0);

            var children = tree.GetChildrenAtLevel(2, tree.Root);

            Assert.AreEqual(8, children.Count);
            Assert.AreEqual(3, tree.Depth);
        }

        [TestMethod]
        public void StoreTypeInTree()
        {
            List<List<List<List<string>>>> megaList = new List<List<List<List<string>>>>();

            TreeNode<Type> tree = new TreeNode<Type>();
            
        }
    }
}
