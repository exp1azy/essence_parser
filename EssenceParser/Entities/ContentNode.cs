using System.Text;

namespace EssenceParser.Entities
{
    /// <summary>
    /// Represents a node in a hierarchical HTML-like content tree.
    /// Each node corresponds to an HTML element or a textual block, and can contain child nodes recursively.
    /// </summary>
    public class ContentNode
    {
        /// <summary>
        /// Gets or sets the type of content this node represents (e.g., text, image, header).
        /// </summary>
        public ContentType Type { get; set; }

        /// <summary>
        /// Gets or sets the textual content of the node, if applicable.
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Gets or sets the value of the "class" attribute of the HTML element.
        /// </summary>
        public string? ClassName { get; set; }

        /// <summary>
        /// Gets or sets the value of the "id" attribute of the HTML element.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the HTML tag represented by this node.
        /// </summary>
        public HtmlTag Tag { get; set; }

        /// <summary>
        /// Gets or sets the collection of HTML attributes associated with this node.
        /// </summary>
        public Dictionary<string, string>? Attributes { get; set; }

        /// <summary>
        /// Gets or sets the list of child nodes nested within this node.
        /// </summary>
        public List<ContentNode> Children { get; set; } = [];

        /// <summary>
        /// Recursively computes the maximum depth of the subtree starting from this node.
        /// A node without children has depth 1.
        /// </summary>
        /// <returns>The maximum depth of the node tree.</returns>
        public int MaxDepth()
        {
            if (Children.Count == 0)
                return 1;
            
            return 1 + Children.Max(c => c.MaxDepth());
        }

        /// <summary>
        /// Recursively counts the total number of nodes in the subtree, including this node.
        /// </summary>
        /// <returns>Total count of this node and all descendant nodes.</returns>
        public int GetChildrenCount()
        {
            return 1 + Children.Sum(c => c.GetChildrenCount());
        }

        /// <summary>
        /// Recursively searches the subtree and returns all nodes that match the given predicate.
        /// </summary>
        /// <param name="predicate">A function that defines the matching condition.</param>
        /// <returns>A list of nodes that satisfy the predicate.</returns>
        public List<ContentNode> FindNodes(Func<ContentNode, bool> predicate)
        {
            var result = new List<ContentNode>();

            if (predicate(this))
                result.Add(this);

            Children.ForEach(c => result.AddRange(c.FindNodes(predicate)));

            return result;
        }

        /// <summary>
        /// Applies a transformation function to each immediate child node.
        /// If the function returns null, the original node is retained.
        /// </summary>
        /// <param name="transformer">A function that transforms a child node.</param>
        /// <returns>The current <see cref="ContentNode"/> instance with transformed children.</returns>
        public ContentNode Replace(Func<ContentNode, ContentNode?> transformer)
        {
            Children = Children.Select(c => transformer(c) ?? c).ToList();
            return this;
        }

        /// <summary>
        /// Recursively removes child nodes that match the given predicate.
        /// </summary>
        /// <param name="predicate">A function defining which nodes to remove.</param>
        /// <returns>The current <see cref="ContentNode"/> instance with pruned children.</returns>
        public ContentNode Purge(Func<ContentNode, bool> predicate)
        {
            if (Children.Count == 0)
                return this;

            for (int i = Children.Count - 1; i >= 0; i--)
            {
                var child = Children[i];
                child.Purge(predicate);
                    
                if (predicate(child))               
                    Children.RemoveAt(i);               
            }

            return this;
        }

        /// <summary>
        /// Creates a deep copy of the current node and all its descendants.
        /// </summary>
        /// <returns>A new <see cref="ContentNode"/> instance that is a clone of the current one.</returns>
        public ContentNode Clone()
        {
            return new ContentNode
            {
                Type = Type,
                Content = Content,
                ClassName = ClassName,
                Id = Id,
                Tag = Tag,
                Attributes = Attributes?.ToDictionary(k => k.Key, v => v.Value),
                Children = Children.Select(c => c.Clone()).ToList()
            };
        }

        /// <summary>
        /// Extracts and concatenates all textual content in the node and its descendants,
        /// omitting any HTML structure.
        /// </summary>
        /// <returns>A plain text representation of the node's content.</returns>
        public string ToPlainText()
        {
            var parts = new List<string>();

            if (!string.IsNullOrWhiteSpace(Content))
                parts.Add(Content);

            parts.AddRange(Children.Select(c => c.ToPlainText()));

            return string.Join(" ", parts.Where(p => !string.IsNullOrWhiteSpace(p))).Trim();
        }

        /// <summary>
        /// Serializes the node and its children into an HTML string.
        /// </summary>
        /// <param name="indentLevel">Optional indentation level for pretty-printing.</param>
        /// <returns>Formatted HTML string representing the node hierarchy.</returns>
        public string ToHtmlString(int indentLevel = 0)
        {
            var indentation = new string(' ', indentLevel * 2);
            var sb = new StringBuilder();

            string tagName = Tag.ToString().ToLowerInvariant();

            if (Tag == HtmlTag.NoTag)
            {
                if (!string.IsNullOrWhiteSpace(Content))
                    sb.AppendLine($"{indentation}{System.Net.WebUtility.HtmlEncode(Content)}");

                foreach (var child in Children)
                    sb.Append(child.ToHtmlString(indentLevel));

                return sb.ToString();
            }

            var attributes = new List<string>();
            if (Attributes != null)
            {
                foreach (var attribute in Attributes)
                    attributes.Add($"{attribute.Key}=\"{attribute.Value}\"");
            }

            string attrString = attributes.Count > 0 ? " " + string.Join(" ", attributes) : string.Empty;
            var selfClosing = new[] { HtmlTag.Br, HtmlTag.Hr, HtmlTag.Img, HtmlTag.Input, HtmlTag.Meta, HtmlTag.Link, HtmlTag.Source, HtmlTag.Area, HtmlTag.Base, HtmlTag.Col, HtmlTag.Embed, HtmlTag.Wbr, HtmlTag.Param, HtmlTag.Track };

            if (selfClosing.Contains(Tag))
            {
                sb.AppendLine($"{indentation}<{tagName}{attrString} />");
                return sb.ToString();
            }

            sb.Append($"{indentation}<{tagName}{attrString}>");

            if (!string.IsNullOrWhiteSpace(Content))
                sb.Append(System.Net.WebUtility.HtmlEncode(Content));

            if (Children.Count > 0)
            {
                sb.AppendLine();

                foreach (var child in Children)
                    sb.Append(child.ToHtmlString(indentLevel + 1));

                sb.AppendLine($"{indentation}</{tagName}>");
            }
            else
            {
                sb.AppendLine($"</{tagName}>");
            }

            return sb.ToString();
        }
    }
}
