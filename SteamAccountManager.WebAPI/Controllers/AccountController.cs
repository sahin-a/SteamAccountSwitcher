using Microsoft.AspNetCore.Mvc;
using SteamAccountManager.Domain.Steam.Model;
using SteamAccountManager.Domain.Steam.UseCase;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SteamAccountManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IGetAccountsWithDetailsUseCase _getAccountsWithDetailsUseCase;

        public AccountController(IGetAccountsWithDetailsUseCase getAccountsUseCase)
        {
            _getAccountsWithDetailsUseCase = getAccountsUseCase;
        }

        [HttpGet]
        public async Task<IEnumerable<Account>> Get()
        {
            return await _getAccountsWithDetailsUseCase.Execute();
        }
    }
}
