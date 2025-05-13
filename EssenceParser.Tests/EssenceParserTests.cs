using EssenceParser.Entities;

namespace EssenceParser.Tests
{
    public class EssenceParserTests
    {
        private static EssenceHtmlParser CreateParser(string html, ParsingOptions options = null)
        {
            var parser = new EssenceHtmlParser(options ?? new ParsingOptions());
            parser.ReadFromString(html);
            return parser;
        }

        [Fact]
        public async Task ParseAsync_ShouldParseSimpleParagraph()
        {
            var html = "<html><body><p>Hello World</p></body></html>";
            var parser = CreateParser(html);

            var result = await parser.ParseAsync();

            Assert.Single(result.Roots);
            var p = result.Roots[0].Children[0];
            Assert.Contains("Hello World", p.Content);
        }

        [Fact]
        public async Task ParseAsync_ShouldIgnoreTag_FromIgnoredTags()
        {
            var html = "<html><body><script>alert('x')</script><p>Keep me</p></body></html>";
            var options = new ParsingOptions
            {
                IgnoredTags = ["script"]
            };
            var parser = CreateParser(html, options);

            var result = await parser.ParseAsync();

            Assert.Single(result.Roots);
            Assert.Equal(HtmlTag.P, result.Roots[0].Tag);
        }

        [Fact]
        public async Task ParseAsync_ShouldRemoveNodesWithBlacklistedClass()
        {
            var html = "<html><body><div class=\"ad-banner\">Ad</div><p>Content</p></body></html>";
            var options = new ParsingOptions
            {
                BlacklistClassPatterns = ["ad-banner"]
            };
            var parser = CreateParser(html, options);

            var result = await parser.ParseAsync();

            Assert.Single(result.Roots);
            Assert.Equal(HtmlTag.P, result.Roots[0].Tag);
            Assert.Contains("Content", result.Roots[0].Children[0].Content);
        }

        [Fact]
        public async Task ParseAsync_ShouldReturnEmptyTree_ForBodylessHtml()
        {
            var html = "<html><head><title>No body</title></head></html>";
            var parser = CreateParser(html);

            var result = await parser.ParseAsync();

            Assert.Empty(result.Roots);
        }

        [Fact]
        public async Task ParseAsync_ShouldNormalizeWhitespace_IfOptionEnabled()
        {
            var html = "<html><body><p>  Hello    \n   World  </p></body></html>";
            var options = new ParsingOptions
            {
                NormalizeWhitespace = true
            };

            var parser = CreateParser(html, options);

            var result = await parser.ParseAsync();
            var text = result.Roots[0].Children[0].Content;

            Assert.Equal("Hello World", text);
        }

        [Fact]
        public async Task ParseAsync_ShouldDecodeHtmlEntities_IfOptionEnabled()
        {
            var html = "<html><body><p>Fish &amp; Chips</p></body></html>";
            var options = new ParsingOptions
            {
                DecodeHtmlEntities = true
            };

            var parser = CreateParser(html, options);
            var result = await parser.ParseAsync();

            Assert.Contains("Fish & Chips", result.Roots[0].Children[0].Content);
        }

        [Fact]
        public async Task ParseAsync_ShouldPreserveNestedStructure()
        {
            var html = "<html><body><div><p>Text</p></div></body></html>";
            var parser = CreateParser(html);

            var result = await parser.ParseAsync();

            Assert.Single(result.Roots);
            var div = result.Roots[0];
            Assert.Equal(HtmlTag.Div, div.Tag);
            Assert.Single(div.Children);
            Assert.Equal(HtmlTag.P, div.Children[0].Tag);
        }

        [Fact]
        public async Task ParseAsync_ShouldFilterAriaHidden_IfOptionEnabled()
        {
            var html = "<html><body><div aria-hidden=\"true\">Invisible</div><p>Visible</p></body></html>";
            var options = new ParsingOptions
            {
                IgnoreAriaHidden = true
            };

            var parser = CreateParser(html, options);
            var result = await parser.ParseAsync();

            Assert.Single(result.Roots);
            Assert.Equal(HtmlTag.P, result.Roots[0].Tag);
        }

        [Fact]
        public async Task ReadFromFileAsync_ShouldThrow_WhenPathIsEmpty()
        {
            var parser = new EssenceHtmlParser(new ParsingOptions());

            await Assert.ThrowsAsync<Exception>(() => parser.ReadFromFileAsync(""));
        }

        [Fact]
        public void ReadFromString_ShouldThrow_WhenHtmlIsEmpty()
        {
            var parser = new EssenceHtmlParser(new ParsingOptions());

            Assert.Throws<Exception>(() => parser.ReadFromString(""));
        }
    }
}
