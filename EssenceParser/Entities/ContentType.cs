namespace EssenceParser.Entities
{
    /// <summary>
    /// Represents the semantic type of content in an HTML element or structure.
    /// Used to classify nodes in a <see cref="ContentNode"/> tree.
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// The content type is unknown or unclassified.
        /// </summary>
        Unknown,

        /// <summary>
        /// A heading element (e.g., &lt;h1&gt; to &lt;h6&gt;).
        /// </summary>
        Heading,

        /// <summary>
        /// A paragraph of text (&lt;p&gt;).
        /// </summary>
        Paragraph,

        /// <summary>
        /// A list element (&lt;ul&gt;, &lt;ol&gt;, &lt;dl&gt;).
        /// </summary>
        List,

        /// <summary>
        /// An individual list item (&lt;li&gt;, &lt;dt&gt;, &lt;dd&gt;).
        /// </summary>
        ListItem,

        /// <summary>
        /// An entire HTML table (&lt;table&gt;).
        /// </summary>
        Table,

        /// <summary>
        /// A row in a table (&lt;tr&gt;).
        /// </summary>
        TableRow,

        /// <summary>
        /// A data cell in a table (&lt;td&gt;).
        /// </summary>
        TableCell,

        /// <summary>
        /// A header cell in a table (&lt;th&gt;).
        /// </summary>
        TableHeader,

        /// <summary>
        /// The body section of a table (&lt;tbody&gt;).
        /// </summary>
        TableBody,

        /// <summary>
        /// The footer section of a table (&lt;tfoot&gt;).
        /// </summary>
        TableFoot,

        /// <summary>
        /// A blockquote element (&lt;blockquote&gt;).
        /// </summary>
        Blockquote,

        /// <summary>
        /// A block of preformatted or inline code (&lt;pre&gt;, &lt;code&gt;).
        /// </summary>
        Code,

        /// <summary>
        /// Inline-level elements not otherwise classified.
        /// </summary>
        Inline,

        /// <summary>
        /// An image element (&lt;img&gt;).
        /// </summary>
        Image,

        /// <summary>
        /// A video element (&lt;video&gt;).
        /// </summary>
        Video,

        /// <summary>
        /// An audio element (&lt;audio&gt;).
        /// </summary>
        Audio,

        /// <summary>
        /// A hyperlink (&lt;a&gt;).
        /// </summary>
        Link,

        /// <summary>
        /// Metadata elements such as &lt;meta&gt;, &lt;title&gt;, or &lt;base&gt;.
        /// </summary>
        Metadata,

        /// <summary>
        /// Sectioning elements like &lt;section&gt;, &lt;nav&gt;, &lt;article&gt;, &lt;aside&gt;.
        /// </summary>
        Sectioning,

        /// <summary>
        /// Form control elements like &lt;input&gt;, &lt;textarea&gt;, &lt;select&gt;.
        /// </summary>
        FormControl,

        /// <summary>
        /// A line break (&lt;br&gt;).
        /// </summary>
        Break,

        /// <summary>
        /// A horizontal rule (&lt;hr&gt;).
        /// </summary>
        HorizontalRule,

        /// <summary>
        /// A label for a form control (&lt;label&gt;).
        /// </summary>
        Label,

        /// <summary>
        /// A button element (&lt;button&gt;).
        /// </summary>
        Button,

        /// <summary>
        /// A media source tag (&lt;source&gt;, &lt;track&gt;).
        /// </summary>
        MediaSource,

        /// <summary>
        /// Mathematical, scientific, or technical markup (e.g., &lt;math&gt; or similar).
        /// </summary>
        MathOrTechnical,

        /// <summary>
        /// A style or script tag (&lt;style&gt;, &lt;script&gt;).
        /// </summary>
        StyleOrScript,

        /// <summary>
        /// A generic container for grouping content (&lt;div&gt;, &lt;main&gt;).
        /// </summary>
        Container,

        /// <summary>
        /// A short inline quotation (&lt;q&gt;).
        /// </summary>
        Quote,

        /// <summary>
        /// Emphasized text (&lt;em&gt;).
        /// </summary>
        Emphasis,

        /// <summary>
        /// Strongly emphasized text (&lt;strong&gt;).
        /// </summary>
        Strong,

        /// <summary>
        /// Subscript text (&lt;sub&gt;).
        /// </summary>
        Subscript,

        /// <summary>
        /// Superscript text (&lt;sup&gt;).
        /// </summary>
        Superscript,

        /// <summary>
        /// Highlighted or marked text (&lt;mark&gt;).
        /// </summary>
        Marked,

        /// <summary>
        /// A time-related element (&lt;time&gt;).
        /// </summary>
        Time,

        /// <summary>
        /// A term definition (&lt;dfn&gt;).
        /// </summary>
        Definition,

        /// <summary>
        /// An abbreviation or acronym (&lt;abbr&gt;).
        /// </summary>
        Abbreviation,

        /// <summary>
        /// A citation or reference (&lt;cite&gt;).
        /// </summary>
        Citation,

        /// <summary>
        /// User keyboard input (&lt;kbd&gt;).
        /// </summary>
        KeyboardInput,

        /// <summary>
        /// A sample or output from a program (&lt;samp&gt;).
        /// </summary>
        SampleOutput,

        /// <summary>
        /// A variable name (&lt;var&gt;).
        /// </summary>
        Variable,

        /// <summary>
        /// A figure caption (&lt;caption&gt;).
        /// </summary>
        Caption,

        /// <summary>
        /// A summary for a collapsible section (&lt;summary&gt;).
        /// </summary>
        Summary,

        /// <summary>
        /// A collapsible section container (&lt;details&gt;).
        /// </summary>
        Details,

        /// <summary>
        /// A figure container (&lt;figure&gt;).
        /// </summary>
        Figure,

        /// <summary>
        /// A caption associated with a figure (&lt;figcaption&gt;).
        /// </summary>
        Figcaption,

        /// <summary>
        /// A generic inline container (&lt;span&gt;).
        /// </summary>
        Span,

        /// <summary>
        /// An input element in a form (&lt;input&gt;).
        /// </summary>
        Input,

        /// <summary>
        /// An HTML template element (&lt;template&gt;).
        /// </summary>
        Template,

        /// <summary>
        /// A plain text node (text content not enclosed by a tag).
        /// </summary>
        Text
    }
}
