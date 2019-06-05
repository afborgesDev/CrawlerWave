using System;
using CrabsWave.Core.Configurations;
using CrabsWave.Core.Functionalities.Elements;
using CrabsWave.Core.Functionalities.Navegation;

namespace CrabsWave.Core
{
    public interface ICrawler : IDisposable
    {
        ICrawler Initializate(Behavior behavior);
        ICrawlerNavigation Navigation();
        ICrawlerElements Elements();
    }
}
