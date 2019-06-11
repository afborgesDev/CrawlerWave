using System;
using CrabsWave.Core.Configurations;
using CrabsWave.Core.Functionalities.Elements;
using CrabsWave.Core.Functionalities.Interactions;
using CrabsWave.Core.Functionalities.Navegation;
using CrabsWave.Core.Functionalities.Scripts;

namespace CrabsWave.Core
{
    public interface ICrawler : IDisposable
    {
        ICrawler Initializate(Behavior behavior);
        ICrawlerNavigation Navigation();
        ICrawlerElements Elements();
        ICrawlerClick Click();
        ICrawlerScripts Scripts();
    }
}
