using SteamAccountManager.Domain.Steam.Model;

namespace SteamAccountManager.Domain.Steam.UseCase
{
    public interface IGetAccountsWithDetailsUseCase
    {
        Task<List<Account>> Execute();
    }
}