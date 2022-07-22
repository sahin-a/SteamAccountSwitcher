using Microsoft.AspNetCore.Mvc;
using SteamAccountManager.Application.Steam.UseCase;
using SteamAccountManager.WebAPI.Dtos;

namespace SteamAccountManager.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SwitcherController : ControllerBase
{
    private readonly SwitchAccountUseCase _switchAccountUseCase;

    public SwitcherController(SwitchAccountUseCase switchAccountUseCase)
    {
        _switchAccountUseCase = switchAccountUseCase;
    }

    [HttpPost]
    public async Task Post(SwitchAccountDto dto)
    {
        await _switchAccountUseCase.Execute(dto.AccountName);
    }
}

