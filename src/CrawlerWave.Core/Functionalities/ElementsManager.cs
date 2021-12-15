using System.Collections.ObjectModel;
using CrawlerWave.Core.Resources;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace CrawlerWave.Core.Functionalities;

internal class ElementsManager : BaseManager
{
    internal const int DefaultNumberOfAttemptsOnRetry = 5;
    internal const int OneAttempt = 1;

    internal const string ScriptRemoveElementUsingId = "document.getElementById('{0}').remove();";
    internal const string ScriptRemoveElementUsingClassName = "document.getElementsByClassName('{0}')[0].remove();";
    private const string LoggerCategoryName = "CrawlerWave.ElementsManager";
    private readonly ElementsType[] AllowedWebElementTypeToRemove = new ElementsType[] { ElementsType.Id, ElementsType.ClassName };

    public ElementsManager(ILogger logger) : base(LoggerCategoryName, logger)
    {
    }

    public static ElementsManager New(Crawler parent) => new ElementsManager(parent.CreateLogger(LoggerCategoryName));

    public IWebElement TryGetElement(Crawler parent, WebElementType webElementType)
    {
        if (webElementType == null)
        {
            Logger.LogError("WebElementType should not be null");
            return null;
        }

        var attemps = DefaultNumberOfAttemptsOnRetry;
        if (!webElementType.ShouldRetry) attemps = OneAttempt;

        IWebElement foundElement;
        for (var i = 1; i <= attemps; i++)
        {
            try
            {
                foundElement = parent.Driver.FindElement(webElementType.ByElement);
                if (foundElement != null)
                    return foundElement;
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not get the element using identify: {webElementType.Identify} and type: {webElementType.ElementType} at the attempt: {i}", e);
            }
        }

        return null;
    }

    public ReadOnlyCollection<IWebElement> TryGetElements(Crawler parent, WebElementType webElementType)
    {
        if (webElementType == null)
        {
            Logger.LogError("WebElementType should not be null");
            return default;
        }

        var attemps = DefaultNumberOfAttemptsOnRetry;
        if (!webElementType.ShouldRetry) attemps = OneAttempt;

        ReadOnlyCollection<IWebElement> elements;
        for (var i = 1; i <= attemps; i++)
        {
            try
            {
                elements = parent.Driver.FindElements(webElementType.ByElement);
                if (elements != null)
                    return elements;
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not get the elements using identify: {webElementType.Identify} and type: {webElementType.ElementType} at the attempt: {i}", e);
            }
        }

        return default;
    }

    public string TryGetAttribute(Crawler parent, WebElementType webElementType, string attribute)
    {
        var element = TryGetElement(parent, webElementType);
        if (element == null) return string.Empty;
        var attemps = DefaultNumberOfAttemptsOnRetry;
        if (!webElementType.ShouldRetry) attemps = OneAttempt;

        for (var i = 1; i <= attemps; i++)
        {
            try
            {
                return element.GetAttribute(attribute);
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not get the attribute for the element {webElementType.Identify}", e);
            }
        }

        return string.Empty;
    }

    public void RemoveElement(Crawler parent, WebElementType webElement)
    {
        if (webElement == null)
        {
            Logger.LogInformation("An ID or ClassName are required to remove an element");
            return;
        }

        if (!AllowedWebElementTypeToRemove.Contains(webElement.ElementType))
        {
            Logger.LogInformation("The WebElementType should be a ID or ClassName");
            return;
        }

        var element = TryGetElement(parent, webElement);
        if (element == null) return;

        if (webElement.ElementType == ElementsType.Id)
        {
            ScriptManager.New(parent).ExecuteScriptUsingJavaScriptExecutor(parent, string.Format(ScriptRemoveElementUsingId, webElement.Identify));
            return;
        }

        if (webElement.ElementType == ElementsType.ClassName)
            ScriptManager.New(parent).ExecuteScriptUsingJavaScriptExecutor(parent, string.Format(ScriptRemoveElementUsingClassName, webElement.Identify));
    }
}
