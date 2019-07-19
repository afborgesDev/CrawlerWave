using System;
using CrabsWave.Core.Functionalities.Scripts;
using CrabsWave.Core.Resources;
using Microsoft.Extensions.Logging;

namespace CrabsWave.Core.Functionalities
{
    internal class ClickManager
    {
        public const string LoggerCategory = "CrawlerWave.TextManager";
        private readonly ILogger Logger;

        public ClickManager(ILogger logger) => Logger = logger;

        public void Click(Crawler parent, WebElementType webElementType, bool shouldRetry)
        {
            var element = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                              .TryGetElement(parent, webElementType, shouldRetry);
            element?.Click();
        }

        public void ClickFirst(Crawler parent, WebElementType webElementType, bool shouldRetry)
        {
            var elements = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                               .TryGetElements(parent, webElementType, shouldRetry);
            try
            {
                elements[0].Click();
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not click at the first element: {webElementType.Identify}. ", e);
            }
        }

        public void ClickUsingJavaScript(Crawler parent, WebElementType webElementType, bool shouldRetry)
        {
            switch (webElementType.ElementType)
            {
                case ElementsType.Id:
                    new ScriptManager(parent.CreateLogger(ScriptManager.LoggerCategory))
                        .ExecuteScriptUsingJavaScriptExecutor(parent, $"document.getElementById('{webElementType.Identify}').click();");
                    break;

                default:
                {
                    var element = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                                     .TryGetElement(parent, webElementType, shouldRetry);

                    new ScriptManager(parent.CreateLogger(ScriptManager.LoggerCategory))
                        .ExecuteScriptUsingJavaScriptExecutor(parent, "(arguments[0] || {click:() => ''}).click();", element);
                    break;
                }
            }
        }
    }
}
