using EssenceParser.Entities;

namespace EssenceParser.Tests
{
    public class ContentNodeTests
    {
        [Fact]
        public void MaxDepth_ShouldReturnCorrectDepth()
        {
            var root = new ContentNode
            {
                Children =
                {
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
                }
            };

            int depth = root.MaxDepth();
            Assert.Equal(4, depth);
        }

        [Fact]
        public void GetChildrenCount_ShouldReturnTotalIncludingSelf()
        {
            var root = new ContentNode
            {
                Children =
                {
                    new ContentNode(),
                    new ContentNode
                    {
                        Children =
                        {
                            new ContentNode(),
                            new ContentNode()
                        }
                    }
                }
            };

            int count = root.GetChildrenCount();
            Assert.Equal(5, count);
        }

        [Fact]
        public void FindNodes_ShouldReturnAllMatching()
        {
            var root = new ContentNode
            {
                Content = "root",
                Children =
                {
                    new ContentNode { Content = "a" },
                    new ContentNode
                    {
                        Content = "b",
                        Children =
                        {
                            new ContentNode { Content = "a" }
                        }
                    }
                }
            };

            var result = root.FindNodes(n => n.Content == "a");
            Assert.Equal(2, result.Count);
            Assert.All(result, n => Assert.Equal("a", n.Content));
        }

        [Fact]
        public void Purge_ShouldRemoveUnmatchedNodesRecursively()
        {
            var root = new ContentNode
            {
                Content = "keep",
                Children =
                {
                    new ContentNode { Content = "remove" },
                    new ContentNode
                    {
                        Content = "keep",
                        Children =
                        {
                            new ContentNode { Content = "remove" },
                            new ContentNode { Content = "keep" }
                        }
                    }
                }
            };

            root.Purge(n => n.Content == "remove");

            Assert.Single(root.Children);
            Assert.Equal("keep", root.Children[0].Content);
            Assert.Single(root.Children[0].Children);
        }

        [Fact]
        public void ToHtmlString_ShouldRenderSelfClosingTag()
        {
            var node = new ContentNode
            {
                Tag = HtmlTag.Img,
                Attributes = new Dictionary<string, string>
                {
                    ["src"] = "image.png",
                    ["alt"] = "Image"
                }
            };

            string html = node.ToHtmlString();
            Assert.Contains("<img src=\"image.png\" alt=\"Image\" />", html);
        }

        [Fact]
        public void ToHtmlString_ShouldRenderNestedTagsWithContent()
        {
            var node = new ContentNode
            {
                Tag = HtmlTag.Div,
                Children =
                {
                    new ContentNode
                    {
                        Tag = HtmlTag.P,
                        Content = "Hello"
                    }
                }
            };

            string html = node.ToHtmlString();
            Assert.Contains("<div>", html);
            Assert.Contains("<p>Hello</p>", html);
        }

        [Fact]
        public void ToHtmlString_ShouldEscapeHtmlContent()
        {
            var node = new ContentNode
            {
                Content = "<b>bold</b>"
            };

            string html = node.ToHtmlString();
            Assert.Contains("&lt;b&gt;bold&lt;/b&gt;", html);
        }

        [Fact]
        public void ToHtmlString_ShouldNotRenderEmptyTags()
        {
            var node = new ContentNode
            {
                Tag = HtmlTag.Div,
                Content = null,
                Children = []
            };

            string html = node.ToHtmlString();
            Assert.Contains("<div></div>", html.Replace("\r", "").Replace("\n", ""));
        }

        [Fact]
        public void ToHtmlString_ShouldIndentNestedElements()
        {
            var node = new ContentNode
            {
                Tag = HtmlTag.Ul,
                Children =
                {
                    new ContentNode
                    {
                        Tag = HtmlTag.Li,
                        Content = "Item 1"
                    },
                    new ContentNode
                    {
                        Tag = HtmlTag.Li,
                        Content = "Item 2"
                    }
                }
            };

            string html = node.ToHtmlString();
            Assert.Contains("  <li>Item 1</li>", html);
            Assert.Contains("  <li>Item 2</li>", html);
        }
    }
}
