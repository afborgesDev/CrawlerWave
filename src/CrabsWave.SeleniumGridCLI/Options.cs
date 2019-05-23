using CommandLine;

namespace CrabsWave.SeleniumGridCLI
{
    public class Options
    {
        [Option('v', "verbose", HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Option('b', "Browsers", Required = true, HelpText = "Kind of browsers to setup.")]
        public SupportedHubBrowsers HubBrowsers { get; set; }

        [Option('i', "Instances", Default = 10, HelpText = "Set the Max Number of Nodes Instances for each hub connected.")]
        public int MaxNodeInstances { get; set; }

        [Option('n', "network", Default = "seleniumNetwork", HelpText = "Set the docker seleniunm network for grid and nodes.")]
        public string SeleniumNetwork { get; set; }

    }
}
