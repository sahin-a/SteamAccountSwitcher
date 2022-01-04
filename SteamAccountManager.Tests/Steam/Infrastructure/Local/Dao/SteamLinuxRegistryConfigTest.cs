using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Infrastructure.Local.Dao
{
    public class SteamLinuxRegistryConfigTest
    {
        private readonly SteamLinuxRegistryConfig _sut;

        public SteamLinuxRegistryConfigTest()
        {
            _sut = new SteamLinuxRegistryConfig();
        }

        [Fact]
        public void Data_gets_Parsed_Correctly()
        {

        }
    }
}
