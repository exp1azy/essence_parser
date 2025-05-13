using EssenceParser.Entities;

namespace EssenceParser.Tests
{
    public class ContentNodeTreeTests
    {
        [Fact]
        public void MaxDepth_ShouldReturnZero_WhenEmpty()
        {
            var tree = new ContentNodeTree();
            Assert.Equal(0, tree.MaxDepth());
        }

        [Fact]
        public void MaxDepth_ShouldReturnCorrectDepth()
        {
            var tree = new ContentNodeTree
            {
                Roots =
                [
                    new ContentNode(),
                    new ContentNode
                    {
                        Children =
                        {
                            new ContentNode
                            {
                                Children =
                                {
                                    new ContentNode()
                                }
                            }
                        }
                    }
                ]
            };

            Assert.Equal(3, tree.MaxDepth());
        }

        [Fact]
        public void GetNodeCount_ShouldReturnTotalIncludingRoots()
        {
            var tree = new ContentNodeTree
            {
                Roots =
                [
                    new ContentNode(),
                    new ContentNode
                    {
                        Children =
                        {
                            new ContentNode(),
                            new ContentNode()
                        }
                    }
                ]
            };

            Assert.Equal(4, tree.GetNodeCount());
        }

        [Fact]
        public void FindNodes_ShouldReturnOnlyMatchingNodes()
        {
            var tree = new ContentNodeTree
            {
                Roots =
                [
                    new ContentNode { Content = "keep" },
                    new ContentNode
                    {
                        Content = "remove",
                        Children =
                        {
                            new ContentNode { Content = "keep" }
                        }
                    }
                ]
            };

            tree.FindNodes(n => n.Content == "keep");

            Assert.Equal(2, tree.Roots.Count);
            Assert.All(tree.Roots, n => Assert.Equal("keep", n.Content));
        }

        [Fact]
        public void Purge_ShouldRemoveNodesRecursively()
        {
            var tree = new ContentNodeTree
            {
                Roots =
                [
                    new ContentNode
                    {
                        Content = "root",
                        Children =
                        {
                            new ContentNode { Content = "keep" },
                            new ContentNode { Content = "remove" }
                        }
                    }
                ]
            };

            tree.Purge(n => n.Content == "remove");

            var root = tree.Roots[0];
            Assert.Single(root.Children);
            Assert.Equal("keep", root.Children[0].Content);
        }

        [Fact]
        public void ToHtmlString_ShouldRenderAllRoots()
        {
            var tree = new ContentNodeTree
            {
                Roots =
                [
                    new ContentNode { Tag = HtmlTag.P, Content = "Hello" },
                    new ContentNode { Tag = HtmlTag.P, Content = "World" }
                ]
            };

            string html = tree.ToHtmlString();
            Assert.Contains("<p>Hello</p>", html);
            Assert.Contains("<p>World</p>", html);
        }

        [Fact]
        public void FindNodes_ShouldReplaceRootsWithMatched()
        {
            var tree = new ContentNodeTree
            {
                Roots =
                [
                    new ContentNode
                    {
                        Content = "no",
                        Children =
                        {
                            new ContentNode { Content = "yes" }
                        }
                    }
                ]
            };

            tree.FindNodes(n => n.Content == "yes");

            Assert.Single(tree.Roots);
            Assert.Equal("yes", tree.Roots[0].Content);
        }
    }
}
