using System;

using CommandLine;

using ShadowTester.CommandLine;
using ShadowTesterLib.Recorder;

namespace ShadowTester
{
    internal static class ConfigurationParser
    {
        internal static RecordConfiguration ParseConfiguration(string[] args)
        {
            CommandLineParser parser = new CommandLineParser();
            CommandLineOptions options = new CommandLineOptions();

            if (parser.ParseArguments(args, options))
            {
                RecordConfiguration result = CreateConfigFromOptions(options);
                RecordValidator recordValidator = new RecordValidator();

                try
                {
                    recordValidator.Validate(result);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

                return result;
            }

            return null;
        }

        static RecordConfiguration CreateConfigFromOptions(CommandLineOptions options)
        {
            RecordConfiguration configuration = new RecordConfiguration
            {
                Name = options.Name,
                CapturesPath = options.CapturesPath,
                Fps = options.Fps,
                RunFromConsole = options.Console
            };

            configuration.InitializeProcesses(options.Processes);
            return configuration;
        }
    }
}
