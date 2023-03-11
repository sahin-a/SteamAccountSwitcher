using Autofac;
using ReactiveUI;
using SteamAccountManager.AvaloniaUI.Common;
using System.Reactive;

namespace SteamAccountManager.AvaloniaUI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        // The Router associated with this Screen.
        // Required by the IScreen interface.
        public RoutingState Router { get; } = new RoutingState();

        // The command that navigates a user to first view model.
        public ReactiveCommand<Unit, IRoutableViewModel> ShowSwitcher { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> ShowSettings { get; }

        // The command that navigates a user back.
        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;

        private readonly ViewModelStore _viewModelStore = new ViewModelStore();

        public MainWindowViewModel()
        {
            // Manage the routing state. Use the Router.Navigate.Execute
            // command to navigate to different view models. 
            //
            // Note, that the Navigate.Execute method accepts an instance 
            // of a view model, this allows you to pass parameters to 
            // your view models, or to reuse existing view models.
            //
            ShowSwitcher = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(_viewModelStore.Get<AccountSwitcherViewModel>(this))
            );

            ShowSettings = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(_viewModelStore.Get<SettingsViewModel>(this))
            );

            Router.Navigate.Execute(_viewModelStore.Get<AccountSwitcherViewModel>(this));
        }
    }
}
