# CrawlerWave
A fluent Lib to wrap selenium and other utilities for a web crawler.

|Pipeline|
|---|
|[![.NET](https://github.com/afborgesDev/CrawlerWave/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/afborgesDev/CrawlerWave/actions/workflows/dotnet.yml)|

[![NuGet](https://img.shields.io/nuget/v/CrawlerWave.Core?maxAge=86400)](https://www.nuget.org/packages/CrawlerWave.Core/)

| Metrics                                                                                                                                                                                    |                                                                                                                                                                                                         |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [![Bugs](https://sonarcloud.io/api/project_badges/measure?project=afborgesDev_CrawlerWave&metric=bugs)](https://sonarcloud.io/dashboard?id=afborgesDev_CrawlerWave)                        | [![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=afborgesDev_CrawlerWave&metric=code_smells)](https://sonarcloud.io/dashboard?id=afborgesDev_CrawlerWave)                       |
| [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=afborgesDev_CrawlerWave&metric=coverage)](https://sonarcloud.io/dashboard?id=afborgesDev_CrawlerWave)                | [![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=afborgesDev_CrawlerWave&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=afborgesDev_CrawlerWave) |
| [![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=afborgesDev_CrawlerWave&metric=ncloc)](https://sonarcloud.io/dashboard?id=afborgesDev_CrawlerWave)              | [![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=afborgesDev_CrawlerWave&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=afborgesDev_CrawlerWave)           |
| [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=afborgesDev_CrawlerWave&metric=alert_status)](https://sonarcloud.io/dashboard?id=afborgesDev_CrawlerWave) | [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=afborgesDev_CrawlerWave&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=afborgesDev_CrawlerWave)         |
| [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=afborgesDev_CrawlerWave&metric=security_rating)](https://sonarcloud.io/dashboard?id=afborgesDev_CrawlerWave)  | [![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=afborgesDev_CrawlerWave&metric=sqale_index)](https://sonarcloud.io/dashboard?id=afborgesDev_CrawlerWave)                    |
| [![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=afborgesDev_CrawlerWave&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=afborgesDev_CrawlerWave)  |


# Usage

Simple Url nagigation example:

```csharp
var crawler = new Crawler(logFactory);

crawler.Initialize(new Behavior())
       .GoToUrl("https://github.com/afborgesDev/CrawlerWave", out _);
```

Get Element text
```csharp
var crawler = new Crawler(logFactory);

crawler.Initialize(new Behavior())
       .GoToUrl("https://github.com/afborgesDev/CrawlerWave", out _)
       .ElementInnerText(WebElement.xPath("//*[@id='js-repo-pjax-container']/div[1]/div/h1"), out var elementText);
```

More Examples on Tests or on Wiki(WIP)
