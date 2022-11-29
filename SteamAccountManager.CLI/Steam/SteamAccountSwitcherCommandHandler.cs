using CLAP;
using SteamAccountManager.CLI.Steam;
using SteamAccountManager.CLI.Steam.Exceptions;

public class SteamAccountSwitcherCommandHandler
{
    private readonly ListCommand _listCommand;
    private readonly SwitchCommand _switchCommand;

    public SteamAccountSwitcherCommandHandler(ListCommand listCommand, SwitchCommand switchCommand)
    {
        _listCommand = listCommand;
        _switchCommand = switchCommand;
    }

    [Verb(IsDefault = true, Description = "Lists all accounts tracked by steam")]
    public int List()
    {
        _listCommand.ListAccounts().Wait();
        return 0;
    }

    [Verb(Description = "Switches to the specified account name")]
    public int Switch([Required][Aliases("n")][Description("account name to switch to")] string accountName)
    {
        var result = -1;
        try
        {
            result = _switchCommand.Switch(accountName).Result;
        }
        catch (InvalidAccountNameException)
        {
        }

        return result;
    }
}
