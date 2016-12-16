using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octopus.Tree;
using Octopus.TypeWalker;

namespace Octopus_Unit_Test
{
    [TestClass]
    public class TreeUnitTest
    {
        [TestMethod]
        public void CreateTreeAndDfsTraversalWorksAsExpected()
        {
            List<int> answerList = new List<int> { 21, 22, 11, 30, 12, 100 };
            int i = 0;

            Tree<int> tree = new Tree<int>(100);
            tree.AddNode(11, 0, 0);
            tree.AddNode(12, 0, 0);
            tree.AddNode(21, 1, 0);
            tree.AddNode(22, 1, 0);
            tree.AddNode(30, 1, 1);

            var nodes = tree.Walker.Traverse(WalkType.Dfs).ToList();

            foreach (var item in answerList)
            {
                Assert.AreEqual(item, nodes[i++].Data);
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
        public void ConstructsCorrectTreeRepresentationOfType()
        {
            var parseThisType = new List<Dictionary<List<string>, Dictionary<int, string>>>();

            TypeTreeConstructor typeTreeConstructor = new TypeTreeConstructor(parseThisType.GetType());
            var tree = typeTreeConstructor.ConstructTypeTree();

            Assert.AreEqual(3, tree.Depth);
            Assert.AreEqual(7, tree.TotalNodes);
            Assert.AreEqual(1, tree.Root.Childrens.Count);
        }

        [TestMethod]
        public void DfsTreeWalker()
        {
            var parseThisType = new List<Dictionary<List<string>, Dictionary<int, string>>>();

            TypeTreeConstructor typeTreeConstructor = new TypeTreeConstructor(parseThisType.GetType());
            var tree = typeTreeConstructor.ConstructTypeTree();

            Walker<Type> walker = new Walker<Type>(tree);
            var result = walker.Traverse(WalkType.Dfs);

            foreach (var treeNode in result)
            {
                Debug.WriteLine(treeNode.Data.Name);
            }
        }

        [TestMethod]
        public void BfsTreeWalker()
        {
            var parseThisType = new List<Dictionary<List<string>, Dictionary<int, string>>>();

            TypeTreeConstructor typeTreeConstructor = new TypeTreeConstructor(parseThisType.GetType());
            var tree = typeTreeConstructor.ConstructTypeTree();

            Walker<Type> walker = new Walker<Type>(tree);
            var result = walker.Traverse(WalkType.Bfs);

            foreach (var treeNode in result)
            {
                Debug.WriteLine(treeNode.Data.Name);
            }
        }

        [TestMethod]
        public void DfsTest()
        {
            var tree = CreateTree(1);
            var output = tree.Walker.Traverse(WalkType.Dfs).Select(t => t.Data).ToList();
            var expected = new[] { 10, 11, 12, 13, 14, 100 };
            CollectionEqual(expected, output);

            tree = CreateTree(2);
            output = tree.Walker.Traverse(WalkType.Dfs).Select(t => t.Data).ToList();
            expected = new[] { 21, 22, 10, 11, 30, 31, 32, 12, 35, 36, 37, 13, 14, 100 };
            CollectionEqual(expected, output);

            tree = CreateTree(5);
            output = tree.Walker.Traverse(WalkType.Dfs).Select(t => t.Data).ToList();
            expected = new[] { 21, 22, 10, 11, 60, 50, 40, 30, 31, 51, 41, 42, 32, 12, 35, 43, 36, 52, 44, 37, 13, 14, 100 };
            CollectionEqual(expected, output);

            tree = CreateTree(0);
            output = tree.Walker.Traverse(WalkType.Dfs).Select(t => t.Data).ToList();
            expected = new[] { 100 };
            CollectionEqual(expected, output);
        }

        [TestMethod]
        public void BfsTest()
        {
            var tree = CreateTree(1);
            var output = tree.Walker.Traverse(WalkType.Bfs).Select(t => t.Data).ToList();
            var expected = new[] { 100, 10, 11, 12, 13, 14 };
            CollectionEqual(expected, output);

            tree = CreateTree(2);
            output = tree.Walker.Traverse(WalkType.Bfs).Select(t => t.Data).ToList();
            expected = new[] { 100, 10, 11, 12, 13, 14, 21, 22, 30, 31, 32, 35, 36, 37 };
            CollectionEqual(expected, output);

            tree = CreateTree(3);
            output = tree.Walker.Traverse(WalkType.Bfs).Select(t => t.Data).ToList();
            expected = new[] { 100, 10, 11, 12, 13, 14, 21, 22, 30, 31, 32, 35, 36, 37, 40, 41, 42, 43, 44 };
            CollectionEqual(expected, output);

            tree = CreateTree(5);
            output = tree.Walker.Traverse(WalkType.Bfs).Select(t => t.Data).ToList();
            expected = new[] { 100, 10, 11, 12, 13, 14, 21, 22, 30, 31, 32, 35, 36, 37, 40, 41, 42, 43, 44, 50, 51, 52, 60 };
            CollectionEqual(expected, output);
        }

        [TestMethod]
        public void TopDownTest()
        {
            var tree = CreateTree(1);
            var output = tree.Walker.Traverse(WalkType.TopDown).Select(t => t.Data).ToList();
            var expected = new[] { 100, 10, 11, 12, 13, 14 };
            CollectionEqual(expected, output);

            tree = CreateTree(2);
            output = tree.Walker.Traverse(WalkType.TopDown).Select(t => t.Data).ToList();
            expected = new[] { 100, 10, 21, 22, 11, 12, 30, 31, 32, 13, 35, 36, 37, 14 };
            CollectionEqual(expected, output);

            tree = CreateTree(3);
            output = tree.Walker.Traverse(WalkType.TopDown).Select(t => t.Data).ToList();
            expected = new[] { 100, 10, 21, 22, 11, 12, 30, 40, 31, 32, 41, 42, 13, 35, 36, 43, 37, 44, 14 };
            CollectionEqual(expected, output);

            tree = CreateTree(5);
            output = tree.Walker.Traverse(WalkType.TopDown).Select(t => t.Data).ToList();
            expected = new[] { 100, 10, 21, 22, 11, 12, 30, 40, 50, 60, 31, 32, 41, 51, 42, 13, 35, 36, 43, 37, 44, 52, 14 };
            CollectionEqual(expected, output);
        }

        private Tree<int> CreateTree(int levels)
        {
            Tree<int> tree = new Tree<int>(100);

            if (levels >= 1)
            {
                AddLevel1(tree);
            }

            if (levels >= 2)
            {
                AddLevel2(tree);
            }

            if (levels >= 3)
            {
                AddLevel3(tree);
            }

            if (levels >= 4)
            {
                AddLevel4(tree);
            }

            if (levels >= 5)
            {
                AddLevel5(tree);
            }

            return tree;
        }

        private void AddLevel5(Tree<int> tree)
        {
            //level 5
            tree.AddNode(60, 4, 0);
        }

        private void AddLevel4(Tree<int> tree)
        {
            //level 4
            tree.AddNode(50, 3, 0);
            tree.AddNode(51, 3, 1);
            tree.AddNode(52, 3, 4);
        }

        private void AddLevel3(Tree<int> tree)
        {
            //level 3
            tree.AddNode(40, 2, 2);
            tree.AddNode(41, 2, 4);
            tree.AddNode(42, 2, 4);
            tree.AddNode(43, 2, 6);
            tree.AddNode(44, 2, 7);
        }

        private void AddLevel2(Tree<int> tree)
        {
            //level 2
            tree.AddNode(21, 1, 0);
            tree.AddNode(22, 1, 0);
            tree.AddNode(30, 1, 2);
            tree.AddNode(31, 1, 2);
            tree.AddNode(32, 1, 2);
            tree.AddNode(35, 1, 3);
            tree.AddNode(36, 1, 3);
            tree.AddNode(37, 1, 3);
        }

        private void AddLevel1(Tree<int> tree)
        {
            //level 1
            tree.AddNode(10, 0, 0);
            tree.AddNode(11, 0, 0);
            tree.AddNode(12, 0, 0);
            tree.AddNode(13, 0, 0);
            tree.AddNode(14, 0, 0);
        }

        private void CollectionEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            if (expected.Count() != actual.Count())
            {
                Assert.IsFalse(true, $"count of expected and actual does not match. expected count = {expected.Count()}, got count = {actual.Count()}");
            }
            int i = 0;
            var got = actual.ToArray();

            foreach (var eo in expected)
            {
                Assert.AreEqual(eo, got[i++]);
            }
        }

    }
}
