using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class SelectsExtension
    {
        public static Crawler SelectByText(this Crawler parent, string identify, ElementsType elementsType, string textToSelect, bool shouldRetry)
        {
            parent.RestoreLog();
            SelectManager.SelectByText(parent.Driver, identify, elementsType, textToSelect, shouldRetry);
            return parent;
        }
    }
}
