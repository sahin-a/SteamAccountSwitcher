using Microsoft.AspNetCore.Mvc;
using SteamAccountManager.Application.Steam.UseCase;
using SteamAccountManager.Domain.Steam.Model;
using SteamAccountManager.WebAPI.Dtos;

namespace SteamAccountManager.WebAPI.Controllers.Steam;

[ApiController]
[Route("[controller]")]
public class SwitcherController : ControllerBase
{
    private readonly IGetAccountsWithDetailsUseCase _getAccountsWithDetailsUseCase;
    private readonly SwitchAccountUseCase _switchAccountUseCase;

    public SwitcherController(
        IGetAccountsWithDetailsUseCase getAccountsWithDetailsUseCase,
        SwitchAccountUseCase switchAccountUseCase
    )
    {
        _getAccountsWithDetailsUseCase = getAccountsWithDetailsUseCase;
        _switchAccountUseCase = switchAccountUseCase;
    }

    [HttpGet(Name = "GetAccounts")]
    public async Task<IEnumerable<Account>> GetAccounts()
    {
        return await _getAccountsWithDetailsUseCase.Execute();
    }

    [HttpGet(Name = "Switch")]
    public async Task Switch(SwitchAccountDto dto)
    {
        await _switchAccountUseCase.Execute(dto.AccountName);
    }
}
