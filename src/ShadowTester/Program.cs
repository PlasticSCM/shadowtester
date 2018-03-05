using System;
using System.Diagnostics;
using System.IO;

using ShadowTesterLib;
using ShadowTesterLib.Recorder;
using ShadowTesterLib.Storage;

using ShadowTester.CommandLine;
using ShadowTester.CommandLine.Commands;

namespace ShadowTester.Presentation.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                string sessionName =
                    "shadowtesting-session-" + DateTime.Now.ToString("yyyyMMdd-hhmm");

                string directory = Path.Combine(
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        "shadowtesting"),
                    sessionName);

                args = new string[] {
                    "--name=" + sessionName,
                    "--path=" + directory,
                    "--fps=2",
                    "--processes=plastic,gluon,bplastic,bgluon"
                };
            }

            RecordConfiguration recordConfiguration;
            if ((recordConfiguration = ConfigurationParser.ParseConfiguration(args)) == null)
            {
                Console.WriteLine(CommandLineOptions.HELP);
                return;
            }

            StorageManager storageManager = new StorageManager();
            Factory.ProcessRecorder.Configure(recordConfiguration);

            try
            {
                storageManager.CreateCapturesDirectory(recordConfiguration.CapturesPath);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            if (!recordConfiguration.RunFromConsole)
            {
                WindowsTrayApp.Run(recordConfiguration);
                return;
            }

            Run(recordConfiguration);
        }

        static void Run(RecordConfiguration recordConfiguration)
        {
            Factory.ProcessRecorder.Start();

            Console.WriteLine("Recording on directory {0}...", recordConfiguration.CapturesPath);
            Console.WriteLine();

            Console.WriteLine("Press ENTER to finish and create the video");
            Console.ReadLine();

            Console.WriteLine("Session video will be created at {0}",
                recordConfiguration.CapturesPath);

            ConsoleCommand command = ConsoleCommandFactory.CreateCommand(
                ConsoleHelper.STOP_RECORDING_ACTION, recordConfiguration);
            command.Execute();

            Console.WriteLine("Press ENTER to quit");
            Console.ReadLine();

            Process.Start(recordConfiguration.CapturesPath);
        }
    }
}
