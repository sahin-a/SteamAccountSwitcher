using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SteamAccountManager.AvaloniaUI.ViewModels;
using SteamAccountManager.AvaloniaUI.Views;

namespace SteamAccountManager.AvaloniaUI
{
    public class App : Avalonia.Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindowView
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}