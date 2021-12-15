namespace CrawlerWave.Core.Resources;

/// <summary>
/// Represents the type of elements that the crawler can search on a loaded webpage
/// </summary>
public enum ElementsType
{
    Id = 0,
    Name = 1,
    TagName = 2,
    ClassName = 3,
    CssSelector = 4,
    LinkText = 5,
    PartialLinkText = 6,
    XPath = 7
}
