using Microsoft.AspNetCore.Mvc;
using SteamAccountManager.Domain.Steam.UseCase;
using SteamAccountManager.WebAPI.Dtos;

namespace SteamAccountManager.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SwitcherController : ControllerBase
{
    private readonly ISwitchAccountUseCase _switchAccountUseCase;

    public SwitcherController(ISwitchAccountUseCase switchAccountUseCase)
    {
        _switchAccountUseCase = switchAccountUseCase;
    }

    [HttpPost]
    public async Task Post(SwitchAccountDto dto)
    {
        await _switchAccountUseCase.Execute(dto.AccountName);
    }
}

