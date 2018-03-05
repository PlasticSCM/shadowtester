using System.Collections.Generic;
using CommandLine;

namespace ShadowTester.CommandLine
{
    public class CommandLineOptions
    {
        [Option(null, "name", Required = true)]
        public string Name;

        [Option(null, "path", Required = true)]
        public string CapturesPath;

        [Option(null, "fps", Required = true)]
        public int Fps;

        [OptionList(null, "processes", Separator = ',', Required = true)] 
        public IList<string> Processes;

        [Option(null, "console", Required = false)]
        public bool Console;

        public const string HELP = "shadowtestercli.exe --name=name --path=path --fps=fps --processes=\"process1,process2...\" --console";
    }
}