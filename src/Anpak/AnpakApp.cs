namespace Anpak;

/// <summary>
/// Main application class for Anpak - .NET 9 packaging application
/// </summary>
public class AnpakApp
{
    public async Task<int> RunAsync(string[] args)
    {
        Console.WriteLine("Anpak - .NET 9 Packaging Application");
        Console.WriteLine("=====================================");

        if (args.Length == 0)
        {
            ShowHelp();
            return 0;
        }

        var command = args[0].ToLowerInvariant();
        
        return command switch
        {
            "pack" => await PackAsync(args.Skip(1).ToArray()),
            "help" or "--help" or "-h" => ShowHelp(),
            _ => ShowUnknownCommand(command)
        };
    }

    private async Task<int> PackAsync(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Error: Project path required");
            Console.WriteLine("Usage: anpak pack <project-path>");
            return 1;
        }

        var projectPath = args[0];
        
        if (!File.Exists(projectPath))
        {
            Console.WriteLine($"Error: Project file not found: {projectPath}");
            return 1;
        }

        Console.WriteLine($"Packaging project: {projectPath}");
        
        var packager = new Packager();
        var result = await packager.PackAsync(projectPath);
        
        if (result.Success)
        {
            Console.WriteLine($"Package created successfully: {result.OutputPath}");
            return 0;
        }
        else
        {
            Console.WriteLine($"Error: {result.Error}");
            return 1;
        }
    }

    private int ShowHelp()
    {
        Console.WriteLine();
        Console.WriteLine("Usage: anpak <command> [options]");
        Console.WriteLine();
        Console.WriteLine("Commands:");
        Console.WriteLine("  pack <project-path>  Package a .NET 9 application");
        Console.WriteLine("  help                 Show this help message");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  anpak pack MyApp.csproj");
        Console.WriteLine("  anpak pack /path/to/MyApp.csproj");
        return 0;
    }

    private int ShowUnknownCommand(string command)
    {
        Console.WriteLine($"Error: Unknown command '{command}'");
        Console.WriteLine("Run 'anpak help' for usage information");
        return 1;
    }
}
