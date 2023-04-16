using Moq;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Infrastructure.Local.DataSource;

public class FileDataSourceTest
{
    private readonly Mock<IFileProvider> _fileProviderMock;
    private readonly FileDataSource _sut;

    public FileDataSourceTest()
    {
        _fileProviderMock = new();

        _sut = new(directory: "Storages", _fileProviderMock.Object);
    }


    [Fact]
    public async void GIVEN_can_write_to_file_successfully_WHEN_storing_value_THEN_returns_true()
    {
        // GIVEN
        _fileProviderMock.Setup(x => x.WriteAllText(@"Storages\test.json", It.IsAny<string>(), false))
            .ReturnsAsync(true);
        // WHEN
        var result = await _sut.Store("test", "Sam Fisher");
        // THEN
        Assert.True(result);
    }

    [Fact]
    public async void GIVEN_fails_to_write_to_file_WHEN_storing_value_THEN_returns_false()
    {
        // GIVEN
        _fileProviderMock.Setup(x => x.WriteAllText(It.IsAny<string>(), It.IsAny<string>(), false))
            .ReturnsAsync(false);
        // WHEN
        var result = await _sut.Store("test", "Sam Fisher");
        // THEN
        Assert.False(result);
    }

    [Fact]
    public async void GIVEN_content_is_available_WHEN_retrieving_value_THEN_returns_value()
    {
        // GIVEN
        _fileProviderMock.Setup(x => x.ReadAllText(@"Storages\test.json"))
            .ReturnsAsync("Elias");
        // WHEN
        var result = await _sut.Load("test");
        // THEN
        Assert.Equal(expected: "Elias", actual: result);
    }


    [Fact]
    public async void GIVEN_content_is_unavailable_WHEN_retrieving_value_THEN_returns_null()
    {
        // GIVEN
        _fileProviderMock.Setup(x => x.ReadAllText(@"Storages\test.json"))
            .ReturnsAsync(value: null);
        // WHEN
        var result = await _sut.Load("test");
        // THEN
        Assert.Null(result);
    }
}