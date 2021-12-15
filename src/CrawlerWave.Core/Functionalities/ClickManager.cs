using CrawlerWave.Core.Resources;
using Microsoft.Extensions.Logging;

namespace CrawlerWave.Core.Functionalities;

//TODO: Should revisit this class seems it can be simplified
internal class ClickManager : BaseManager
{
    private const string LoggerCategoryName = "CrawlerWave.TextManager";

    public ClickManager(ILogger logger) : base(LoggerCategoryName, logger)
    {
    }

    public static ClickManager New(Crawler parent) => new(parent.CreateLogger(LoggerCategoryName));

    public void ClickUsingJavaScript(Crawler parent, WebElementType webElementType)
    {
        if (webElementType.ElementType == ElementsType.Id)
        {
            ScriptManager.New(parent).ExecuteScriptUsingJavaScriptExecutor(parent, $"document.getElementById('{webElementType.Identify}').click();");
        }
        else
        {
            var element = ElementsManager.New(parent).TryGetElement(parent, webElementType);
            ScriptManager.New(parent).ExecuteScriptUsingJavaScriptExecutor(parent, "(arguments[0] || {click:() => ''}).click();", element);
        }
    }

    public void Click(Crawler parent, WebElementType webElementType)
    {
        var element = ElementsManager.New(parent).TryGetElement(parent, webElementType);
        element?.Click();
    }

    public void ClickFirst(Crawler parent, WebElementType webElementType)
    {
        var elements = ElementsManager.New(parent).TryGetElements(parent, webElementType);
        try
        {
            elements[0].Click();
        }
        catch (Exception e)
        {
            Logger?.LogError($"Could not click at the first element: {webElementType.Identify}. ", e);
        }
    }
}
