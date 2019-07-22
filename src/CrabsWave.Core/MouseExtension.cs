using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class MouseExtension
    {
        public static Crawler MouseMove(this Crawler parent, WebElementType webElementType, bool shouldRetry)
        {
            MouseManager.New(parent).MoveTo(parent, webElementType, shouldRetry, false);
            return parent;
        }

        public static Crawler MouseMoveAndClick(this Crawler parent, WebElementType webElementType, bool shouldRetry)
        {
            MouseManager.New(parent).MoveTo(parent, webElementType, shouldRetry, true);
            return parent;
        }
    }
}
