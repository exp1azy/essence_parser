namespace EssenceParser.Entities
{
    /// <summary>
    /// Configuration options for controlling HTML parsing behavior in the <see cref="EssenceHtmlParser"/>.
    /// These options allow customization of allowed tags, ignored elements, content filters,
    /// and parsing rules such as whitespace normalization and HTML entity decoding.
    /// </summary>
    public class ParsingOptions
    {
        /// <summary>
        /// A set of allowed HTML tags (in lowercase) that are considered valid during parsing.
        /// By default, includes all defined tags in the <see cref="HtmlTag"/> enumeration.
        /// Tags not in this list will be ignored unless explicitly processed.
        /// </summary>
        public HashSet<string> AllowedTags { get; set; } = Enum.GetNames<HtmlTag>().Select(tag => tag.ToLowerInvariant()).ToHashSet();

        /// <summary>
        /// A set of HTML tags (in lowercase) that should be explicitly ignored during parsing,
        /// even if they are listed in <see cref="AllowedTags"/>.
        /// </summary>
        public HashSet<string> IgnoredTags { get; set; } = [];

        /// <summary>
        /// A set of class name patterns (as simple string matches or regex-like expressions)
        /// that, when matched, cause the associated HTML element to be excluded from the parsed result.
        /// </summary>
        public HashSet<string> BlacklistClassPatterns { get; set; } = [];

        /// <summary>
        /// Indicates whether to normalize all whitespace in text nodes.
        /// When enabled, multiple spaces, tabs, and line breaks are collapsed into a single space.
        /// </summary>
        public bool NormalizeWhitespace { get; set; } = true;

        /// <summary>
        /// Indicates whether to decode HTML entities (e.g., &amp;amp;, &amp;lt;, etc.) into their corresponding characters.
        /// </summary>
        public bool DecodeHtmlEntities { get; set; } = true;

        /// <summary>
        /// When enabled, hidden HTML elements (with styles such as display: none or visibility: hidden) are ignored during parsing.
        /// </summary>
        public bool IgnoreHiddenElements { get; set; } = false;

        /// <summary>
        /// When enabled, elements with the <c>aria-hidden="true"</c> attribute are excluded from the parsed result.
        /// </summary>
        public bool IgnoreAriaHidden { get; set; } = false;
    }
}
