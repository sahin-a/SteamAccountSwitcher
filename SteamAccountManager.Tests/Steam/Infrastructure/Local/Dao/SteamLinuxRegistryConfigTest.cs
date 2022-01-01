using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;

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
