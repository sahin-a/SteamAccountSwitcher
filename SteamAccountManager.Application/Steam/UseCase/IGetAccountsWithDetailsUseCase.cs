using SteamAccountManager.Domain.Steam.Model;

namespace SteamAccountManager.Application.Steam.UseCase
{
    public interface IGetAccountsWithDetailsUseCase
    {
        Task<List<Account>> Execute();
    }
}