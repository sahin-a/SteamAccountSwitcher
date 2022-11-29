using SteamAccountManager.Application.Steam.UseCase;
using SteamAccountManager.CLI.Steam.Exceptions;
using System.Text.RegularExpressions;

namespace SteamAccountManager.CLI.Steam
{
    public class SwitchCommand
    {
        private readonly ISwitchAccountUseCase _switchAccountUseCase;

        public SwitchCommand(ISwitchAccountUseCase switchAccountUseCase)
        {
            _switchAccountUseCase = switchAccountUseCase;
        }

        public async Task<int> Switch(string accountName)
        {
            var isValid = Regex.IsMatch(accountName, pattern: @"^[a-zA-Z0-9]{3,}$");
            if (!isValid)
                throw new InvalidAccountNameException();

            var result = await _switchAccountUseCase.Execute(accountName);
            if (!result)
            {
                Console.WriteLine("Failed to switch account!");
                return -1;
            }

            return 0;
        }
    }
}
