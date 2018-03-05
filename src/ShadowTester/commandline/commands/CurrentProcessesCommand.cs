using System;
using ShadowTesterLib.Captures;
using ShadowTesterLib.Recorder;

namespace ShadowTester.CommandLine.Commands
{
    public class CurrentProcessesCommand : ConsoleCommand
    {
        private RecordConfiguration configuration;

        public CurrentProcessesCommand(RecordConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public override void Execute()
        {
            Console.WriteLine(String.Join("\n", configuration.ExpectedProcesses));
        }
    }
}