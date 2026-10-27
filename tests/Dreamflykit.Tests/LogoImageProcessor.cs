using Xunit;

namespace Dreamflykit.Tests;

public class LogoImageProcessor
{
    [Fact]
    public void BrandingFiles_ExistAndAreValid()
    {
        var rootDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "src", "Dreamflykit.Web", "wwwroot", "images", "branding"));
        if (!Directory.Exists(rootDir))
        {
            rootDir = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "src", "Dreamflykit.Web", "wwwroot", "images", "branding"));
        }

        Assert.True(Directory.Exists(rootDir), $"Directory must exist: {rootDir}");
        Assert.True(File.Exists(Path.Combine(rootDir, "dreamflykit-d-bob-white.png")), "dreamflykit-d-bob-white.png must exist");
        Assert.True(File.Exists(Path.Combine(rootDir, "dreamflykit-brand-bob-white.png")), "dreamflykit-brand-bob-white.png must exist");
        Assert.True(File.Exists(Path.Combine(rootDir, "dreamflykit-d-symbol-transparent.png")), "dreamflykit-d-symbol-transparent.png must exist");
        Assert.True(File.Exists(Path.Combine(rootDir, "dreamflykit-brand-transparent.png")), "dreamflykit-brand-transparent.png must exist");
    }
}
