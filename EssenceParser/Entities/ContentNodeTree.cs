namespace EssenceParser.Entities
{
    /// <summary>
    /// Represents a hierarchical tree of content nodes, typically parsed from an HTML document.
    /// Acts as the root container for a set of top-level <see cref="ContentNode"/> instances.
    /// </summary>
    public class ContentNodeTree
    {
        /// <summary>
        /// Gets or sets the list of top-level content nodes in the tree.
        /// These nodes are considered the roots of independent subtrees.
        /// </summary>
        public List<ContentNode> Roots { get; set; } = [];

        /// <summary>
        /// Calculates the maximum depth across all root nodes in the tree.
        /// A tree with no nodes has a depth of 0.
        /// </summary>
        /// <returns>The maximum depth of the node hierarchy.</returns>
        public int MaxDepth()
        {
            return Roots.Count == 0 ? 0 : Roots.Max(node => node.MaxDepth());
        }

        /// <summary>
        /// Computes the total number of nodes in the tree, including all descendants.
        /// </summary>
        /// <returns>The total count of all nodes in the tree.</returns>
        public int GetNodeCount()
        {
            return Roots.Sum(node => node.GetChildrenCount());
        }

        /// <summary>
        /// Searches all nodes in the tree and retains only those that match the provided predicate.
        /// This operation modifies the tree in place by flattening it to only matching nodes.
        /// </summary>
        /// <param name="predicate">A function that defines the matching condition.</param>
        /// <returns>The current <see cref="ContentNodeTree"/> instance with filtered nodes.</returns>
        public ContentNodeTree FindNodes(Func<ContentNode, bool> predicate)
        {
            Roots = Roots.SelectMany(node => node.FindNodes(predicate)).ToList();
            return this;
        }

        /// <summary>
        /// Applies a transformation to each root node using the specified function.
        /// The function can replace or modify each node in place.
        /// </summary>
        /// <param name="transformer">A function that returns a replacement or modified node.</param>
        /// <returns>The current <see cref="ContentNodeTree"/> instance with transformed nodes.</returns>
        public ContentNodeTree Replace(Func<ContentNode, ContentNode?> transformer)
        {
            Roots.ForEach(node => node.Replace(transformer));
            return this;
        }

        /// <summary>
        /// Removes all nodes from the tree that match the specified predicate.
        /// The operation is applied recursively to all root nodes and their descendants.
        /// </summary>
        /// <param name="predicate">A function that returns true for nodes that should be removed.</param>
        /// <returns>The current <see cref="ContentNodeTree"/> instance with pruned nodes.</returns>
        public ContentNodeTree Purge(Func<ContentNode, bool> predicate)
        {
            Roots.ForEach(node => node.Purge(predicate));
            return this;
        }

        /// <summary>
        /// Creates a deep copy of the tree, including all nodes and their attributes.
        /// </summary>
        /// <returns>A cloned instance of the <see cref="ContentNodeTree"/>.</returns>
        public ContentNodeTree Clone()
        {
            Roots = Roots.Select(node => node.Clone()).ToList();
            return this;
        }

        /// <summary>
        /// Extracts and concatenates plain text from all nodes in the tree.
        /// HTML tags and structure are omitted.
        /// </summary>
        /// <returns>A plain text representation of the tree content.</returns>
        public string ToPlainText()
        {
            return string.Join("\n", Roots.Select(node => node.ToPlainText()));
        }

        /// <summary>
        /// Serializes the tree into an HTML-formatted string.
        /// This reflects the structure and content of the nodes, including indentation.
        /// </summary>
        /// <returns>HTML string representation of the entire tree.</returns>
        public string ToHtmlString()
        {
            return string.Join("\n", Roots.Select(node => node.ToHtmlString()));
        }
    }
}
