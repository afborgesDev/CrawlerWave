using System;
using CrabsWave.Core.Configurations;
using CrabsWave.Core.Navegation;

namespace CrabsWave.Core
{
    public interface ICrawler : IDisposable
    {
        ICrawler Initializate(Behavior behavior);
        ICrawlerNavigation Navigation();
    }
}
