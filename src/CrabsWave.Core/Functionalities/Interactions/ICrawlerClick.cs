namespace CrabsWave.Core.Functionalities.Interactions
{
    public interface ICrawlerClick
    {
        ICrawler ById(string identify);
        ICrawler ByName(string identify);
        ICrawler ByTagName(string identify);
        ICrawler ByClassName(string identify);
        ICrawler ByCssSelector(string identify);
        ICrawler ByLinkText(string identify);
        ICrawler ByPartialLinkText(string identify);
        ICrawler ByXPath(string identify);

        ICrawler ByIdUsingScript(string identify);
        ICrawler ByNameUsingScript(string identify);
        ICrawler ByTagNameUsingScript(string identify);
        ICrawler ByClassNameUsingScript(string identify);
        ICrawler ByCssSelectorUsingScript(string identify);
        ICrawler ByLinkTextUsingScript(string identify);
        ICrawler ByPartialLinkTextUsingScript(string identify);
        ICrawler ByXPathUsingScript(string identify);

        ICrawler FirstById(string identify);
        ICrawler FirstByName(string identify);
        ICrawler FirstByTagName(string identify);
        ICrawler FirstByClassName(string identify);
        ICrawler FirstByCssSelector(string identify);
        ICrawler FirstByLinkText(string identify);
        ICrawler FirstByPartialLinkText(string identify);
        ICrawler FirstByXPath(string identify);

        ICrawler ByIdIfTrue(string identify, bool condition);
        ICrawler ByNameIfTrue(string identify, bool condition);
        ICrawler ByTagNameIfTrue(string identify, bool condition);
        ICrawler ByClassNameIfTrue(string identify, bool condition);
        ICrawler ByCssSelectorIfTrue(string identify, bool condition);
        ICrawler ByLinkTextIfTrue(string identify, bool condition);
        ICrawler ByPartialLinkTextIfTrue(string identify, bool condition);
        ICrawler ByXPathIfTrue(string identify, bool condition);

        ICrawler ClickAlert(bool acept);
    }
}
