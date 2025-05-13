using EssenceParser.Entities;

namespace EssenceParser
{
    internal class EssenceHtmlMapper
    {
        private static Dictionary<string, HtmlTag> GetTags()
        {
            return Enum.GetValues<HtmlTag>()
                .Cast<HtmlTag>()
                .Where(tag => tag != HtmlTag.NoTag)
                .ToDictionary(
                    tag => tag.ToString().ToLowerInvariant(),
                    tag => tag
                );
        }

        internal static HtmlTag MapToHtmlTag(string? tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                return HtmlTag.NoTag;

            return GetTags().TryGetValue(tagName.ToLowerInvariant(), out var tag)
                ? tag
                : HtmlTag.NoTag;
        }

        internal static ContentType MapToContentType(HtmlTag tag)
        {
            return tag switch
            {
                HtmlTag.H1 or HtmlTag.H2 or HtmlTag.H3 or HtmlTag.H4 or HtmlTag.H5 or HtmlTag.H6 => ContentType.Heading,
                HtmlTag.P => ContentType.Paragraph,
                HtmlTag.Ul or HtmlTag.Ol or HtmlTag.Dl => ContentType.List,
                HtmlTag.Li or HtmlTag.Dt or HtmlTag.Dd => ContentType.ListItem,
                HtmlTag.Table => ContentType.Table,
                HtmlTag.Tr => ContentType.TableRow,
                HtmlTag.Td => ContentType.TableCell,
                HtmlTag.Th => ContentType.TableHeader,
                HtmlTag.Tbody => ContentType.TableBody,
                HtmlTag.Tfoot => ContentType.TableFoot,
                HtmlTag.Blockquote => ContentType.Blockquote,
                HtmlTag.Pre or HtmlTag.Code => ContentType.Code,
                HtmlTag.Span or HtmlTag.I or HtmlTag.B or HtmlTag.U or HtmlTag.Small => ContentType.Inline,
                HtmlTag.Strong => ContentType.Strong,
                HtmlTag.Em => ContentType.Emphasis,
                HtmlTag.Img or HtmlTag.Picture => ContentType.Image,
                HtmlTag.Video => ContentType.Video,
                HtmlTag.Audio => ContentType.Audio,
                HtmlTag.Source or HtmlTag.Track => ContentType.MediaSource,
                HtmlTag.A => ContentType.Link,
                HtmlTag.Br => ContentType.Break,
                HtmlTag.Hr => ContentType.HorizontalRule,
                HtmlTag.Form or HtmlTag.Input or HtmlTag.Textarea or HtmlTag.Select or HtmlTag.Option or HtmlTag.Button or HtmlTag.Label or HtmlTag.Output or HtmlTag.Fieldset or HtmlTag.Legend => ContentType.FormControl,
                HtmlTag.Nav or HtmlTag.Header or HtmlTag.Footer or HtmlTag.Main or HtmlTag.Section or HtmlTag.Article or HtmlTag.Aside => ContentType.Sectioning,
                HtmlTag.Abbr => ContentType.Abbreviation,
                HtmlTag.Cite => ContentType.Citation,
                HtmlTag.Q => ContentType.Quote,
                HtmlTag.Var => ContentType.Variable,
                HtmlTag.Kbd => ContentType.KeyboardInput,
                HtmlTag.Samp => ContentType.SampleOutput,
                HtmlTag.Sub => ContentType.Subscript,
                HtmlTag.Sup => ContentType.Superscript,
                HtmlTag.Mark => ContentType.Marked,
                HtmlTag.Time => ContentType.Time,
                HtmlTag.Dfn => ContentType.Definition,
                HtmlTag.Title or HtmlTag.Meta or HtmlTag.Link or HtmlTag.Base => ContentType.Metadata,
                HtmlTag.Script or HtmlTag.Style or HtmlTag.Noscript or HtmlTag.Template => ContentType.StyleOrScript,
                HtmlTag.Caption => ContentType.Caption,
                HtmlTag.Figure => ContentType.Figure,
                HtmlTag.Figcaption => ContentType.Figcaption,
                HtmlTag.Summary => ContentType.Summary,
                HtmlTag.Details => ContentType.Details,
                HtmlTag.Html or HtmlTag.Head or HtmlTag.Body or HtmlTag.Div => ContentType.Container,
                HtmlTag.Bdi or HtmlTag.Bdo => ContentType.Inline,
                HtmlTag.Canvas or HtmlTag.Embed or HtmlTag.Iframe or HtmlTag.Object or HtmlTag.Map => ContentType.MediaSource,
                HtmlTag.Data or HtmlTag.Meter or HtmlTag.Progress => ContentType.MathOrTechnical,
                HtmlTag.Col or HtmlTag.Colgroup or HtmlTag.Param => ContentType.Metadata,
                HtmlTag.Wbr => ContentType.Break,
                HtmlTag.Ins or HtmlTag.Del => ContentType.Inline,
                _ => ContentType.Unknown
            };
        }
    }
}
