using System;

using ShadowTesterLib.Recorder;

namespace ShadowTester.CommandLine.Commands
{
    public class AddProcessCommand : ConsoleCommand
    {
        private RecordConfiguration configuration;

        public AddProcessCommand(RecordConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public override void Execute()
        {
            Console.WriteLine("Adding a process...");
            configuration.AddProcess(ConsoleHelper.GetProcessFromConsole());
            Console.WriteLine("Process added");
        }
    }
}