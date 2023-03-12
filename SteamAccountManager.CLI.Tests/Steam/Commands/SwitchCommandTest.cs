using Moq;
using SteamAccountManager.CLI.Steam;
using SteamAccountManager.CLI.Steam.Exceptions;
using System;
using System.IO;
using System.Text;
using SteamAccountManager.Domain.Steam.UseCase;
using Xunit;

namespace SteamAccountManager.CLI.Tests.Steam.Commands
{
    public class SwitchCommandTest
    {
        private readonly Mock<ISwitchAccountUseCase> _switchAccountUseCase;
        private readonly StringWriter _outputWriter;
        private readonly StringBuilder _output;
        private readonly SwitchCommand _sut;

        public SwitchCommandTest()
        {
            _outputWriter = new StringWriter();
            Console.SetOut(_outputWriter);
            _output = _outputWriter.GetStringBuilder();
            _output.Clear();

            _switchAccountUseCase = new Mock<ISwitchAccountUseCase>();
            _sut = new SwitchCommand(_switchAccountUseCase.Object);
        }

        [Fact]
        public async void GIVEN_valid_account_name_specified_WHEN_switching_THEN_switch_to_specificed_name_and_return_success_code()
        {
            // GIVEN
            var accountName = "SamFisher";
            _switchAccountUseCase.Setup(x => x.Execute(accountName))
                .ReturnsAsync(true)
                .Verifiable();
            // WHEN
            var result = await _sut.Switch(accountName);
            // THEN
            _switchAccountUseCase.Verify(x => x.Execute(accountName), times: Times.Once);
            Assert.Equal(expected: (int)ExitCode.Success, actual: result);
        }

        [Fact]
        public async void GIVEN_invalid_account_name_entered_WHEN_switching_THEN_throw_exception()
        {
            // GIVEN
            var invalidAccountNames = new string[] { "!Sam", "Sam#", "S*m", "Süm", "Sa" };
            // WHEN
            foreach (var accountName in invalidAccountNames)
            {
                await Assert.ThrowsAsync<InvalidAccountNameException>(() => _sut.Switch(accountName));
            }
            // THEN
            _switchAccountUseCase.Verify(x => x.Execute(It.IsAny<string>()), times: Times.Never);
        }

        [Fact]
        public async void GIVEN_no_account_name_specified_WHEN_switching_THEN_throw_exception()
        {
            // GIVEN
            var accountName = "";
            // WHEN
            await Assert.ThrowsAsync<InvalidAccountNameException>(() => _sut.Switch(accountName));
            // THEN
            _switchAccountUseCase.Verify(x => x.Execute(It.IsAny<string>()), times: Times.Never);
        }

        [Fact]
        public async void GIVEN_switching_is_blocked_WHEN_switching_THEN_print_failure_state_and_return_failure_code()
        {
            // GIVEN
            var accountName = "SamFisher";
            _switchAccountUseCase.Setup(x => x.Execute(accountName))
                .ReturnsAsync(false)
                .Verifiable();
            // WHEN
            var result = await _sut.Switch(accountName);
            // THEN
            _switchAccountUseCase.Verify(x => x.Execute(It.IsAny<string>()), times: Times.Once);
            Assert.Equal(expected: "Failed to switch account!\r\n", actual: _output.ToString());
            Assert.Equal(expected: 1, actual: result);
        }
    }
}
