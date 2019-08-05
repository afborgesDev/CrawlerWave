using System;
using CommandLine;
using CommandLine.Text;

namespace CrabsWave.SeleniumGridCLI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Crab's Wave Selenium Grid CLI!");
            using (var parser = new Parser(with => { with.CaseInsensitiveEnumValues = true; with.CaseSensitive = false; }))
            {
                var result = parser.ParseArguments<Options>(args);

                result//.WithParsed(RunApplication)
                      .WithNotParsed(_ =>
                      {
                          var helpText = HelpText.AutoBuild(result, h =>
                          {
                              h.AdditionalNewLineAfterOption = false;
                              return HelpText.DefaultParsingErrorsHandler(result, h);
                          }, e => e);

                          Console.WriteLine(helpText);

                      });
            }
        }

      
    }
}
