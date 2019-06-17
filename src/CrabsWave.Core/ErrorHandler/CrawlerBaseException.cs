using System;
using System.Diagnostics.CodeAnalysis;

namespace CrabsWave.Core.ErrorHandler
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class CrawlerBaseException : Exception
    {
        public CrawlerBaseException() : base()
        {
        }

        public CrawlerBaseException(string message) : base(message)
        {
        }

        public CrawlerBaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CrawlerBaseException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
