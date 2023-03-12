using SteamAccountManager.Domain.Steam.UseCase;

namespace SteamAccountManager.CLI.Steam
{
    public class ListCommand
    {
        private readonly IGetAccountsWithDetailsUseCase _getAccountsUseCase;

        public ListCommand(IGetAccountsWithDetailsUseCase getAccountsWithDetailsUseCase)
        {
            _getAccountsUseCase = getAccountsWithDetailsUseCase;
        }

        public async Task<int> ListAccounts()
        {
            var accounts = await _getAccountsUseCase.Execute();

            if (accounts.Count == 0)
            {
                Console.WriteLine("No Accounts found!");
                return (int)ExitCode.Failure;
            }

            var accountRows = accounts.ConvertAll(x => $"Account Name: {x.Name}, Username: {x.Username}");
            Console.WriteLine(string.Join("\r\n", accountRows));

            return (int)ExitCode.Success;
        }
    }
}
