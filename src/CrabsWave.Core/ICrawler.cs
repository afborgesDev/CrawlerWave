using System;
using CrabsWave.Core.Configurations;

namespace CrabsWave.Core
{
    public interface ICrawler : IDisposable
    {
        ICrawler Initializate(Behavior behavior);
    }
}
