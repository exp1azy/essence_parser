namespace EssenceParser.Entities
{
    /// <summary>
    /// Represents supported HTML tags mapped from DOM elements.
    /// Used to classify and process HTML elements in the parsing process.
    /// </summary>
    public enum HtmlTag
    {
        /// <summary>No tag assigned (text node or undefined element).</summary>
        NoTag,

        /// <summary>&lt;address&gt; tag — contact information.</summary>
        Address,

        /// <summary>&lt;area&gt; tag — image map area.</summary>
        Area,

        /// <summary>&lt;base&gt; tag — base URL for relative URLs.</summary>
        Base,

        /// <summary>&lt;bdi&gt; tag — bi-directional text isolation.</summary>
        Bdi,

        /// <summary>&lt;bdo&gt; tag — override text direction.</summary>
        Bdo,

        /// <summary>&lt;canvas&gt; tag — scriptable graphics.</summary>
        Canvas,

        /// <summary>&lt;col&gt; tag — column in table.</summary>
        Col,

        /// <summary>&lt;colgroup&gt; tag — group of columns in table.</summary>
        Colgroup,

        /// <summary>&lt;data&gt; tag — machine-readable value.</summary>
        Data,

        /// <summary>&lt;datalist&gt; tag — predefined options for input.</summary>
        Datalist,

        /// <summary>&lt;del&gt; tag — deleted content.</summary>
        Del,

        /// <summary>&lt;dfn&gt; tag — term definition.</summary>
        Dfn,

        /// <summary>&lt;dialog&gt; tag — dialog box or modal.</summary>
        Dialog,

        /// <summary>&lt;embed&gt; tag — external resource integration.</summary>
        Embed,

        /// <summary>&lt;fieldset&gt; tag — group of form controls.</summary>
        Fieldset,

        /// <summary>&lt;form&gt; tag — HTML form container.</summary>
        Form,

        /// <summary>&lt;iframe&gt; tag — embedded frame.</summary>
        Iframe,

        /// <summary>&lt;input&gt; tag — form control.</summary>
        Input,

        /// <summary>&lt;ins&gt; tag — inserted content.</summary>
        Ins,

        /// <summary>&lt;legend&gt; tag — caption for fieldset.</summary>
        Legend,

        /// <summary>&lt;link&gt; tag — external resource reference.</summary>
        Link,

        /// <summary>&lt;map&gt; tag — client-side image map.</summary>
        Map,

        /// <summary>&lt;meta&gt; tag — metadata for the document.</summary>
        Meta,

        /// <summary>&lt;meter&gt; tag — scalar measurement.</summary>
        Meter,

        /// <summary>&lt;noscript&gt; tag — fallback for no JS support.</summary>
        Noscript,

        /// <summary>&lt;object&gt; tag — embedded external resource.</summary>
        Object,

        /// <summary>&lt;optgroup&gt; tag — group of options in a select.</summary>
        Optgroup,

        /// <summary>&lt;option&gt; tag — selectable item.</summary>
        Option,

        /// <summary>&lt;output&gt; tag — result of a calculation.</summary>
        Output,

        /// <summary>&lt;param&gt; tag — parameters for embedded content.</summary>
        Param,

        /// <summary>&lt;progress&gt; tag — progress indicator.</summary>
        Progress,

        /// <summary>&lt;ruby&gt; tag — ruby annotation container.</summary>
        Ruby,

        /// <summary>&lt;rb&gt; tag — ruby base text.</summary>
        Rb,

        /// <summary>&lt;rt&gt; tag — ruby text annotation.</summary>
        Rt,

        /// <summary>&lt;rtc&gt; tag — ruby text container.</summary>
        Rtc,

        /// <summary>&lt;rp&gt; tag — parentheses for ruby text.</summary>
        Rp,

        /// <summary>&lt;s&gt; tag — strikethrough text.</summary>
        S,

        /// <summary>&lt;select&gt; tag — drop-down list.</summary>
        Select,

        /// <summary>&lt;style&gt; tag — embedded CSS.</summary>
        Style,

        /// <summary>&lt;template&gt; tag — reusable content fragment.</summary>
        Template,

        /// <summary>&lt;textarea&gt; tag — multi-line text input.</summary>
        Textarea,

        /// <summary>&lt;title&gt; tag — document title.</summary>
        Title,

        /// <summary>&lt;track&gt; tag — subtitles and media tracks.</summary>
        Track,

        /// <summary>&lt;wbr&gt; tag — word break opportunity.</summary>
        Wbr,

        /// <summary>&lt;html&gt; tag — root of an HTML document.</summary>
        Html,

        /// <summary>&lt;head&gt; tag — container for metadata.</summary>
        Head,

        /// <summary>&lt;body&gt; tag — main document body.</summary>
        Body,

        /// <summary>&lt;section&gt; tag — thematic grouping.</summary>
        Section,

        /// <summary>&lt;article&gt; tag — independent content unit.</summary>
        Article,

        /// <summary>&lt;aside&gt; tag — tangential or complementary content.</summary>
        Aside,

        /// <summary>&lt;nav&gt; tag — navigation links.</summary>
        Nav,

        /// <summary>&lt;header&gt; tag — introductory content.</summary>
        Header,

        /// <summary>&lt;footer&gt; tag — footer content.</summary>
        Footer,

        /// <summary>&lt;main&gt; tag — main content of the document.</summary>
        Main,

        /// <summary>&lt;h1&gt; heading level 1.</summary>
        H1,
        /// <summary>&lt;h2&gt; heading level 2.</summary>
        H2,
        /// <summary>&lt;h3&gt; heading level 3.</summary>
        H3,
        /// <summary>&lt;h4&gt; heading level 4.</summary>
        H4,
        /// <summary>&lt;h5&gt; heading level 5.</summary>
        H5,
        /// <summary>&lt;h6&gt; heading level 6.</summary>
        H6,

        /// <summary>&lt;p&gt; tag — paragraph.</summary>
        P,

        /// <summary>&lt;blockquote&gt; tag — block quotation.</summary>
        Blockquote,

        /// <summary>&lt;pre&gt; tag — preformatted text.</summary>
        Pre,

        /// <summary>&lt;code&gt; tag — code snippet.</summary>
        Code,

        /// <summary>&lt;div&gt; tag — generic block container.</summary>
        Div,

        /// <summary>&lt;ul&gt; tag — unordered list.</summary>
        Ul,

        /// <summary>&lt;ol&gt; tag — ordered list.</summary>
        Ol,

        /// <summary>&lt;li&gt; tag — list item.</summary>
        Li,

        /// <summary>&lt;dl&gt; tag — description list.</summary>
        Dl,

        /// <summary>&lt;dt&gt; tag — term in a description list.</summary>
        Dt,

        /// <summary>&lt;dd&gt; tag — description in a list.</summary>
        Dd,

        /// <summary>&lt;table&gt; tag — table element.</summary>
        Table,

        /// <summary>&lt;thead&gt; tag — table head.</summary>
        Thead,

        /// <summary>&lt;tbody&gt; tag — table body.</summary>
        Tbody,

        /// <summary>&lt;tfoot&gt; tag — table footer.</summary>
        Tfoot,

        /// <summary>&lt;tr&gt; tag — table row.</summary>
        Tr,

        /// <summary>&lt;th&gt; tag — table header cell.</summary>
        Th,

        /// <summary>&lt;td&gt; tag — table data cell.</summary>
        Td,

        /// <summary>&lt;caption&gt; tag — table caption.</summary>
        Caption,

        /// <summary>&lt;figure&gt; tag — self-contained media content.</summary>
        Figure,

        /// <summary>&lt;figcaption&gt; tag — caption for &lt;figure&gt;.</summary>
        Figcaption,

        /// <summary>&lt;img&gt; tag — image element.</summary>
        Img,

        /// <summary>&lt;picture&gt; tag — responsive image container.</summary>
        Picture,

        /// <summary>&lt;video&gt; tag — embedded video.</summary>
        Video,

        /// <summary>&lt;audio&gt; tag — embedded audio.</summary>
        Audio,

        /// <summary>&lt;source&gt; tag — media source.</summary>
        Source,

        /// <summary>&lt;a&gt; tag — hyperlink.</summary>
        A,

        /// <summary>&lt;span&gt; tag — generic inline container.</summary>
        Span,

        /// <summary>&lt;strong&gt; tag — strong importance.</summary>
        Strong,

        /// <summary>&lt;em&gt; tag — emphasis.</summary>
        Em,

        /// <summary>&lt;br&gt; tag — line break.</summary>
        Br,

        /// <summary>&lt;i&gt; tag — italic text.</summary>
        I,

        /// <summary>&lt;b&gt; tag — bold text.</summary>
        B,

        /// <summary>&lt;u&gt; tag — underlined text.</summary>
        U,

        /// <summary>&lt;mark&gt; tag — highlighted text.</summary>
        Mark,

        /// <summary>&lt;small&gt; tag — small text.</summary>
        Small,

        /// <summary>&lt;sub&gt; tag — subscript text.</summary>
        Sub,

        /// <summary>&lt;sup&gt; tag — superscript text.</summary>
        Sup,

        /// <summary>&lt;script&gt; tag — embedded JavaScript.</summary>
        Script,

        /// <summary>&lt;time&gt; tag — machine-readable date/time.</summary>
        Time,

        /// <summary>&lt;hr&gt; tag — thematic break.</summary>
        Hr,

        /// <summary>&lt;abbr&gt; tag — abbreviation or acronym.</summary>
        Abbr,

        /// <summary>&lt;cite&gt; tag — citation.</summary>
        Cite,

        /// <summary>&lt;q&gt; tag — short inline quote.</summary>
        Q,

        /// <summary>&lt;var&gt; tag — variable name.</summary>
        Var,

        /// <summary>&lt;kbd&gt; tag — user input.</summary>
        Kbd,

        /// <summary>&lt;samp&gt; tag — sample output.</summary>
        Samp,

        /// <summary>&lt;label&gt; tag — label for form control.</summary>
        Label,

        /// <summary>&lt;button&gt; tag — clickable button.</summary>
        Button,

        /// <summary>&lt;details&gt; tag — collapsible disclosure widget.</summary>
        Details,

        /// <summary>&lt;summary&gt; tag — summary for details element.</summary>
        Summary
    }
}
