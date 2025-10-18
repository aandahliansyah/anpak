namespace Anpak;

/// <summary>
/// Result of a packaging operation
/// </summary>
public class PackageResult
{
    public bool Success { get; set; }
    public string? OutputPath { get; set; }
    public string? Error { get; set; }
}

/// <summary>
/// Core packager for .NET 9 applications
/// </summary>
public class Packager
{
    public async Task<PackageResult> PackAsync(string projectPath)
    {
        try
        {
            var fullProjectPath = Path.GetFullPath(projectPath);
            var projectDirectory = Path.GetDirectoryName(fullProjectPath) ?? Directory.GetCurrentDirectory();
            
            // Build the project first
            Console.WriteLine("Building project...");
            var buildResult = await BuildProjectAsync(fullProjectPath);
            if (!buildResult.Success)
            {
                return buildResult;
            }

            // Create package
            Console.WriteLine("Creating package...");
            var packagePath = Path.Combine(projectDirectory, "bin", "Release", "net9.0", "publish");
            
            // Publish the project
            var publishResult = await PublishProjectAsync(fullProjectPath, packagePath);
            if (!publishResult.Success)
            {
                return publishResult;
            }

            return new PackageResult
            {
                Success = true,
                OutputPath = packagePath
            };
        }
        catch (Exception ex)
        {
            return new PackageResult
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    private async Task<PackageResult> BuildProjectAsync(string projectPath)
    {
        try
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"build \"{projectPath}\" -c Release",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                return new PackageResult
                {
                    Success = false,
                    Error = $"Build failed: {error}"
                };
            }

            Console.WriteLine("Build completed successfully");
            return new PackageResult { Success = true };
        }
        catch (Exception ex)
        {
            return new PackageResult
            {
                Success = false,
                Error = $"Build error: {ex.Message}"
            };
        }
    }

    private async Task<PackageResult> PublishProjectAsync(string projectPath, string outputPath)
    {
        try
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"publish \"{projectPath}\" -c Release -o \"{outputPath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                return new PackageResult
                {
                    Success = false,
                    Error = $"Publish failed: {error}"
                };
            }

            Console.WriteLine("Publish completed successfully");
            return new PackageResult { Success = true, OutputPath = outputPath };
        }
        catch (Exception ex)
        {
            return new PackageResult
            {
                Success = false,
                Error = $"Publish error: {ex.Message}"
            };
        }
    }
}
