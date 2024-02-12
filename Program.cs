internal class Program
{
    private static WindowManager _wm = new();
    private static bool _running = true;

    private static void Main(string[] args)
    {
		Console.Clear();
		PrintHelp();
        while (_running)
        {
            var userInput = Console.ReadLine()?.Split();
            if (userInput is null)
            {
                Console.WriteLine("Invalid input");
                continue;
            }
            ExecuteCommand(userInput);
        }
        Console.WriteLine("Program ended...");
    }

    private static void ExecuteCommand(string[] args)
    {
        var command = args.First();
        var flags = args.Skip(1).Where(arg => arg.StartsWith('-')).ToArray();
        var arguments = args.Skip(1).ToArray();

		Console.Clear();

        Action action = command switch
        {
            "help" or "h" => PrintHelp,
            "print" or "p" => () => _wm.PrintDebug(),
            "ls" => () => _wm.PrintLayouts(),
            "save" or "s" => () => SaveWindowState(arguments, flags),
            "load" or "l" => () => LoadWindowState(arguments),
            "quit" or "q" or "exit" => () => _running = false,
            _ => () => Console.WriteLine("Command not found, to show available commands: \n'help'")
        };
        action();
    }

    private static void SaveWindowState(string[] arguments, string[] flags)
    {
		if (!arguments.Any())
		{
			Console.WriteLine("No argument provided\n'help' for help");
			return;
		}
        Console.WriteLine("flags" + string.Join('m', flags));
        var ignoreCurrentWindow = flags.Contains("-i");
        Console.WriteLine("ignore" + ignoreCurrentWindow);
        var name = arguments.First();
        _wm.SaveWindowLayout(name, ignoreCurrentWindow);
    }

    private static void LoadWindowState(string[] arguments)
    {
		if (!arguments.Any())
		{
			Console.WriteLine("No argument provided\n'help' for help");
			return;
		}
        var name = arguments.First();
        _wm.SetWindowLayout(name);
    }

    private static void PrintHelp()
    {
        Console.WriteLine($"""

		Usage:
		h|help              Prints this help
		p|print             Prints saved states
		s|save <name> (-i)  Saves current layout as <name>
		                    (use -i flag to ignore currently
		                    active terminal)
		ls                  List all saved layouts
		l|load <name|id>    Loads a window setup
		q|quit              Quits the application 
		                    (exit also works)
		""");
    }
}
