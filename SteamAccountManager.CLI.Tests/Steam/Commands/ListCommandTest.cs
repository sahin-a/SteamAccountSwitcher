using Moq;
using SteamAccountManager.CLI.Steam;
using SteamAccountManager.Domain.Steam.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SteamAccountManager.Domain.Steam.UseCase;
using Xunit;

namespace SteamAccountManager.CLI.Tests.Steam.Commands
{
    public class ListCommandTest
    {
        private readonly Mock<IGetAccountsWithDetailsUseCase> _getAccountsUseCase;
        private readonly StringWriter _outputWriter;
        private readonly StringBuilder _output;
        private readonly ListCommand _sut;

        public ListCommandTest()
        {
            _outputWriter = new StringWriter();
            Console.SetOut(_outputWriter);
            _output = _outputWriter.GetStringBuilder();
            _output.Clear();

            _getAccountsUseCase = new Mock<IGetAccountsWithDetailsUseCase>();
            _sut = new ListCommand(_getAccountsUseCase.Object);
        }

        [Fact]
        public async void GIVEN_no_accounts_available_WHEN_executing_THEN_print_empty_state_and_return_failure_code()
        {
            // GIVEN
            _getAccountsUseCase.Setup(x => x.Execute()).ReturnsAsync(new List<Account>());
            // WHEN
            var result = await _sut.ListAccounts();
            // THEN
            Assert.Equal(expected: "No Accounts found!\r\n", actual: _output.ToString());
            Assert.Equal(expected: 1, actual: result);
        }

        [Fact]
        public async void GIVEN_accounts_available_WHEN_executing_THEN_print_account_list_and_return_success_code()
        {
            // GIVEN
            var accounts = new List<Account>()
            {
                new Account() { Id = "123", Name = "JuliasAccount", Username = "Julia" },
                new Account() { Id = "321", Name = "JuliaAlt", Username = "ailuJ111" }
            };
            _getAccountsUseCase.Setup(x => x.Execute()).ReturnsAsync(accounts);
            // WHEN
            var result = await _sut.ListAccounts();
            // THEN
            Assert.Equal(
                expected: $"Account Name: JuliasAccount, Username: Julia\r\n" +
                $"Account Name: JuliaAlt, Username: ailuJ111\r\n",
                actual: _output.ToString()
            );
            Assert.Equal(expected: 1, actual: result);
        }
    }
}
