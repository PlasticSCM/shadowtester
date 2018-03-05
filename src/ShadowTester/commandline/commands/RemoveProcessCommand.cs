using System;

using ShadowTesterLib.Recorder;

namespace ShadowTester.CommandLine.Commands
{
    public class RemoveProcessCommand : ConsoleCommand
    {
        private RecordConfiguration configuration;

        public RemoveProcessCommand(RecordConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public override void Execute()
        {
            Console.WriteLine("Removing a process...");
            configuration.RemoveProcess(ConsoleHelper.GetProcessFromConsole());
            Console.WriteLine("Process removed");
        }
    }
}