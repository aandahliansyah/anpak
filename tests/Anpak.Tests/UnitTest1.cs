namespace Anpak.Tests;

public class PackagerTests
{
    [Fact]
    public async Task PackAsync_WithInvalidProjectPath_ReturnsFailure()
    {
        // Arrange
        var packager = new Packager();
        var nonExistentPath = "nonexistent.csproj";

        // Act
        var result = await packager.PackAsync(nonExistentPath);

        // Assert
        Assert.False(result.Success);
        Assert.NotNull(result.Error);
    }
}

public class AnpakAppTests
{
    [Fact]
    public async Task RunAsync_WithNoArguments_ShowsHelp()
    {
        // Arrange
        var app = new AnpakApp();

        // Act
        var result = await app.RunAsync(Array.Empty<string>());

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task RunAsync_WithHelpCommand_ReturnsZero()
    {
        // Arrange
        var app = new AnpakApp();

        // Act
        var result = await app.RunAsync(new[] { "help" });

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task RunAsync_WithUnknownCommand_ReturnsOne()
    {
        // Arrange
        var app = new AnpakApp();

        // Act
        var result = await app.RunAsync(new[] { "unknown" });

        // Assert
        Assert.Equal(1, result);
    }
}
