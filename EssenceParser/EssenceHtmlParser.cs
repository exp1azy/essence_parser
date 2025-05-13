using AngleSharp;
using AngleSharp.Css.Dom;
using AngleSharp.Dom;
using AngleSharp.Text;
using EssenceParser.Entities;
using EssenceParser.Exceptions;
using System.Net;
using System.Text.RegularExpressions;

namespace EssenceParser
{
    /// <summary>
    /// Parses HTML content and converts it into a structured <see cref="ContentNodeTree"/> representation.
    /// Supports reading HTML from strings or files and mapping DOM elements to strongly-typed nodes.
    /// </summary>
    public partial class EssenceHtmlParser
    {
        private readonly ParsingOptions _options;
        private string _html;

        /// <summary>
        /// Initializes a new instance of the <see cref="EssenceHtmlParser"/> class using the provided parser options.
        /// </summary>
        /// <param name="config">The parser configuration options.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="config"/> is null.</exception>
        public EssenceHtmlParser(ParsingOptions config)
        {
            ArgumentNullException.ThrowIfNull(config);
            _options = config;
        }

        /// <summary>
        /// Loads HTML content from a string for subsequent parsing.
        /// </summary>
        /// <param name="html">A non-empty HTML string.</param>
        /// <exception cref="HtmlNotDefinedException">Thrown if the HTML input is null or empty.</exception>
        public void ReadFromString(string html)
        {
            if (string.IsNullOrEmpty(html))
                throw new HtmlNotDefinedException();

            _html = html;
        }

        /// <summary>
        /// Asynchronously loads HTML content from a file for parsing.
        /// </summary>
        /// <param name="path">The full file path to the HTML file.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that completes once the file content is loaded into memory.</returns>
        /// <exception cref="PathNotDefinedException">Thrown if the file path is null or empty.</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown if the directory of the provided path does not exist.</exception>
        /// <exception cref="NotHtmlExtensionException">Thrown if the file does not have a `.html` extension.</exception>
        public async Task ReadFromFileAsync(string path, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(path))
                throw new PathNotDefinedException();

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException();

            if (!path.EndsWith(".html"))
                throw new NotHtmlExtensionException();

            string html = await File.ReadAllTextAsync(path, cancellationToken);
            ReadFromString(html);
        }

        /// <summary>
        /// Parses the previously loaded HTML into a <see cref="ContentNodeTree"/> structure.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the parsing operation.</param>
        /// <returns>A task that returns the resulting <see cref="ContentNodeTree"/>.</returns>
        public async Task<ContentNodeTree> ParseAsync(CancellationToken cancellationToken = default)
        {
            var document = await ProcessParcingAsync(cancellationToken);
            var result = new ContentNodeTree();

            var body = document.Body;
            if (body == null) return result;

            foreach (var element in body.Children)
            {
                var node = MapElementToNode(element);

                if (node != null && (!string.IsNullOrWhiteSpace(node.Content) || node.Children.Count > 0))               
                    result.Roots.Add(node);                
            }

            return result;
        }

        private async Task<IDocument> ProcessParcingAsync(CancellationToken cancellationToken)
        {
            var config = Configuration.Default
                .WithDefaultLoader()
                .WithCss();

            var context = BrowsingContext.New(config);
            var source = new TextSource(_html);

            var document = await context.OpenAsync(req => req.Content(source.Text), cancellationToken);
            FilterDom(document);

            return document;
        }

        private void FilterDom(IDocument document)
        {
            var all = document.All.Where(elem => elem is not null).ToList();

            foreach (var elem in all)
            {
                if (_options.IgnoredTags.Contains(elem.TagName.ToLowerInvariant()))
                {
                    elem.Remove();
                    continue;
                }

                string classAttribute = elem.ClassName ?? string.Empty;
                bool matchesBlacklist = _options.BlacklistClassPatterns.Any(p => classAttribute.Contains(p, StringComparison.OrdinalIgnoreCase));

                if (matchesBlacklist)
                {
                    elem.Remove();
                    continue;
                }

                if (_options.IgnoreHiddenElements)
                {
                    var display = elem.ComputeCurrentStyle().GetDisplay();
                    if (display == "none")
                    {
                        elem.Remove();
                        continue;
                    }
                }

                if (_options.IgnoreAriaHidden && elem.GetAttribute("aria-hidden") == "true")
                {
                    elem.Remove();
                    continue;
                }
            }
        }

        private string NormalizeText(string text)
        {
            if (_options.NormalizeWhitespace)
                text = NormalizeWhitespaceRegex().Replace(text, " ").Trim();

            if (_options.DecodeHtmlEntities)
                text = WebUtility.HtmlDecode(text);

            return text;
        }

        private ContentNode MapElementToNode(IElement elem)
        {
            var tagName = elem.TagName.ToLowerInvariant();

            if (_options.IgnoredTags.Contains(tagName))
                return null;

            var isAllowed = _options.AllowedTags.Contains(tagName);
            var tag = EssenceHtmlMapper.MapToHtmlTag(tagName);
            var children = new List<ContentNode>();

            foreach (var node in elem.ChildNodes)
            {
                switch (node)
                {
                    case IElement childElement:
                    {
                        var childNode = MapElementToNode(childElement);
                        if (childNode != null)
                            children.Add(childNode);

                        break;
                    }
                    case IText textNode:
                    {
                        var normalized = NormalizeText(textNode.Text);
                        if (!string.IsNullOrWhiteSpace(normalized))
                        {
                            if (children.LastOrDefault()?.Content != normalized)
                            {
                                children.Add(new ContentNode
                                {
                                    Tag = HtmlTag.NoTag,
                                    Content = normalized,
                                    Type = ContentType.Text
                                });
                            }
                        }

                        break;
                    }
                }
            }

            if (!isAllowed && children.Count > 0)
            {
                if (children.Count == 1)
                    return children[0];

                return new ContentNode
                {
                    Tag = HtmlTag.NoTag,
                    Type = ContentType.Container,
                    Children = children
                };
            }

            return new ContentNode
            {
                Type = EssenceHtmlMapper.MapToContentType(tag),
                Tag = tag,
                Children = children,
                Attributes = elem.Attributes.ToDictionary(attr => attr.Name, attr => attr.Value),
                ClassName = elem.ClassName,
                Id = elem.Id
            };
        }

        [GeneratedRegex(@"\s+")]
        private static partial Regex NormalizeWhitespaceRegex();
    }
}
