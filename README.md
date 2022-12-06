<a href="https://www.buymeacoffee.com/sahina"><img src="https://img.buymeacoffee.com/button-api/?text=Buy me a coffee&emoji=&slug=sahina&button_colour=FFDD00&font_colour=000000&font_family=Bree&outline_colour=000000&coffee_colour=ffffff"></a>

# Steam Account Manager
It lets you switch between accounts seamlessly if their password has been remembered by the steam client. 
No data is being stored by this application.

# Known Issues
App not launching?
- [Install .NET 6 Runtime](https://dotnet.microsoft.com/en-us/download)

# AvaloniaUI Build *(Avalonia Build (0.21 Alpha))*
Current Features:
* Switch Accounts
* Open Profile when clicking the Avatar
* Shows Accounts Details (Account Name, Username, VAC/Game Ban Status, Avatar, Level)
* Offline Functionality

![avalonia_0 2_preview_screenshot](https://user-images.githubusercontent.com/55054756/147849648-7f9735bb-4c5d-475a-b367-1524e1377831.png)


# Developer Notes
enter your api key here => [SteamWebClient.cs:18](https://github.com/sahin-a/SteamAccountManager/blob/fe847849e0e638e179794070bc605e50b65f8e9b/SteamAccountManager.Infrastructure/Steam/Remote/Dao/SteamWebClient.cs#L18)

# Todos
* refactoring
* make the AvaloniaUI Build prettier
* add linux support

# Supported Plattforms
* Windows
* Linux (Coming soon)
