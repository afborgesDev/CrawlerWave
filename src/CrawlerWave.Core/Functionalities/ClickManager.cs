using System;
using CrawlerWave.Core.Resources;
using Microsoft.Extensions.Logging;

namespace CrawlerWave.Core.Functionalities
{
    internal class ClickManager : BaseManager
    {
        public ClickManager(ILogger logger) : base("CrawlerWave.TextManager", logger)
        {
        }

        public static ClickManager New(Crawler parent) => new ClickManager(parent.CreateLogger(LoggerCategory));

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
                Logger.LogError($"Could not click at the first element: {webElementType.Identify}. ", e);
            }
        }

        public void ClickUsingJavaScript(Crawler parent, WebElementType webElementType)
        {
            switch (webElementType.ElementType)
            {
                case ElementsType.Id:
                    ScriptManager.New(parent).ExecuteScriptUsingJavaScriptExecutor(parent, $"document.getElementById('{webElementType.Identify}').click();");
                    break;

                default:
                {
                    var element = ElementsManager.New(parent).TryGetElement(parent, webElementType);
                    ScriptManager.New(parent).ExecuteScriptUsingJavaScriptExecutor(parent, "(arguments[0] || {click:() => ''}).click();", element);
                    break;
                }
            }
        }
    }
}
