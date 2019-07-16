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

        public void Click(Crawler parent, string identify, ElementsType elementsType, bool shouldRetry)
        {
            var element = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                              .TryGetElement(parent, identify, elementsType, shouldRetry);
            element?.Click();
        }

        public void ClickFirst(Crawler parent, string identify, ElementsType elementsType, bool shouldRetry)
        {
            var elements = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                               .TryGetElements(parent, identify, elementsType, shouldRetry);
            try
            {
                elements[0].Click();
            }
            catch (Exception e)
            {
                Logger.LogError($"Could not click at the first element: {identify}. ", e);
            }
        }

        public void ClickUsingJavaScript(Crawler parent, string identify, ElementsType elementsType, bool shouldRetry)
        {
            switch (elementsType)
            {
                case ElementsType.Id:
                    new ScriptManager(parent.CreateLogger(ScriptManager.LoggerCategory))
                        .ExecuteScriptUsingJavaScriptExecutor(parent, $"document.getElementById('{identify}').click();");
                    break;

                default:
                {
                    var element = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                                     .TryGetElement(parent, identify, elementsType, shouldRetry);

                    new ScriptManager(parent.CreateLogger(ScriptManager.LoggerCategory))
                        .ExecuteScriptUsingJavaScriptExecutor(parent, "(arguments[0] || {click:() => ''}).click();", element);
                    break;
                }
            }
        }
    }
}
