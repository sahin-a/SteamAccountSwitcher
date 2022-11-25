using System.IO;
using SteamAccountManager.Infrastructure.Common;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Infrastructure.Common;

public class SafeFileNameConverterTest
{
    private readonly string _invalidChars = new(Path.GetInvalidFileNameChars());

    [Fact]
    public void WHEN_string_contains_invalid_filename_characters_THEN_replace()
    {
        Assert.DoesNotContain(
            _invalidChars,
            new string(Path.GetInvalidFileNameChars()).ToSafeFileName()
        );
    }
}