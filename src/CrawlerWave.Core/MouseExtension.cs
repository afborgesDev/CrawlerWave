using CrawlerWave.Core.Functionalities;
using CrawlerWave.Core.Resources;

namespace CrawlerWave.Core;

public static class MouseExtension
{
    public static Crawler MouseMove(this Crawler parent, WebElementType webElementType)
    {
        MouseManager.New(parent).MoveTo(parent, webElementType, false);
        return parent;
    }

    public static Crawler MouseMoveAndClick(this Crawler parent, WebElementType webElementType)
    {
        MouseManager.New(parent).MoveTo(parent, webElementType, true);
        return parent;
    }
}
