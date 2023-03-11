using ReactiveUI;
using System;
using System.Windows.Input;

namespace SteamAccountManager.AvaloniaUI.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject
    {
    }

    public abstract class RoutableViewModel : ViewModelBase, IRoutableViewModel
    {
        public ICommand NavigateBackCommand { get; }

        public string? UrlPathSegment => Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }

        public RoutableViewModel(IScreen screen)
        {
            NavigateBackCommand = ReactiveCommand.Create(NavigateBack);
            HostScreen = screen;
        }

        protected void NavigateBack()
        {
            HostScreen.Router.NavigateBack.Execute();
        }
    }
}
