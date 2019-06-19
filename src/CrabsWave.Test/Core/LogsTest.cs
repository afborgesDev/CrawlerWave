﻿using System;
using CrabsWave.Core;
using CrabsWave.Core.ErrorHandler;
using CrabsWave.Core.LogsReports;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrabsWave.Test.Core
{
    public class LogsTest
    {
        private const string ExampleMessage = "Example Message";

        [Fact]
        public void ShouldntWriteLogInformation()
        {
            var loggerMoq = new Mock<ILogger<Crawler>>();
            LogManager.Initializate(loggerMoq.Object, false);
            LogManager.LogInformation(ExampleMessage);
            loggerMoq.VerifyLog(LogLevel.Information, ExampleMessage, Times.Never());
        }

        [Fact]
        public void ShouldLogError()
        {
            var logMoq = new Mock<ILogger<Crawler>>();
            LogManager.Initializate(logMoq.Object, false);
            LogManager.LogError(ExampleMessage);
            logMoq.VerifyLog(LogLevel.Error, ExampleMessage, Times.Once());
        }

        //[Fact]
        //public void ShouldErrorWithNoInitialization()
        //{
        //    Action act = () => LogManager.LogError(ExampleMessage);
        //    act.Should().ThrowExactly<CrawlerBaseException>()
        //       .WithMessage("Should initilizate the logger");
        //}
    }
}
