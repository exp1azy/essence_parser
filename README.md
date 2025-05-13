# EssenceParser
**EssenceParser** is a small C# library for parsing, traversing, and transforming HTML documents into structured, strongly-typed content trees. 
It provides an object-oriented API to work with HTML as a tree of `ContentNode` objects, enabling transformation, filtering, serialization, and semantic analysis of HTML content.

## Features
- Convert HTML to a tree structure of semantic content nodes `ContentNodeTree`
- Traverse and manipulate nodes via predicates or transformation functions
- Serialize nodes back to HTML or plain text
- Configure parsing behavior through `ParserOptions`
- Clone, filter, and transform HTML content using LINQ-like operations

## Overview
The main class for parsing is `EssenceHtmlParser`. It contains key methods for working with HTML:
1. `ReadFromString()` - Loads HTML content from a string for subsequent parsing.
2. `ReadFromFileAsync()` - Asynchronously loads HTML content from a file for parsing.
3. `ParseAsync()` - Parses the previously loaded HTML into a `ContentNodeTree` structure.

As a result of parsing we get `ContentNodeTree`, which is a hierarchical tree of content nodes, typically parsed from an HTML document. It acts as the root container for a set of top-level `ContentNode` instances.
The `ContentNodeTree` class, like `ContentNode`, provides a number of methods for working with an object-oriented HTML tree:
1. `MaxDepth()` - Calculates the maximum depth across all root nodes in the tree.
2. `GetNodeCount()` - Computes the total number of nodes in the tree, including all descendants.
3. `FindNodes()` - Searches all nodes in the tree and retains only those that match the provided predicate. This operation modifies the tree in place by flattening it to only matching nodes.
4. `Replace()` - Applies a transformation to each root node using the specified function.
5. `Purge()` - Removes all nodes from the tree that match the specified predicate.
6. `Clone()` - Creates a deep copy of the tree, including all nodes and their attributes.
7. `ToPlainText()` - Extracts and concatenates plain text from all nodes in the tree. HTML tags and structure are omitted.
8. `ToHtmlString()` - Serializes the tree into an HTML-formatted string. This reflects the structure and content of the nodes, including indentation.

## Usage
Create an instance of `ContentNodeTree` and specify the options in the constructor:
```csharp
var parser = new EssenceHtmlParser(new ParsingOptions());
```

Load your HTML from a file or pass a string directly:
```csharp
string html = "<html><body><p>Hello World</p></body></html>";
parser.ReadFromString(html);
// OR
await parser.ReadFromFileAsync("your-path-to-file.html");
```

Now you can parse your HTML in `ContentNodeTree`:
```csharp
var tree = await parser.ParseAsync();
```

You can perform operations on the received `ContentNodeTree`:
```csharp
var scriptNodes = tree.FindNodes(n => n.Tag == HtmlTag.Script);
```
