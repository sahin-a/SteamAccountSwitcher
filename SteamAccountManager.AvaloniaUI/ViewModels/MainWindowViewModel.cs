using Autofac;
using ReactiveUI;
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

        public MainWindowViewModel()
        {
            // Manage the routing state. Use the Router.Navigate.Execute
            // command to navigate to different view models. 
            //
            // Note, that the Navigate.Execute method accepts an instance 
            // of a view model, this allows you to pass parameters to 
            // your view models, or to reuse existing view models.
            //
            var accountSwitcherViewModel = Dependencies.Container?.Resolve<AccountSwitcherViewModel>(new TypedParameter(typeof(IScreen), this))
                ?? throw new System.Exception("Failed to resolve AccountSwitcherViewModel");

            ShowSwitcher = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(accountSwitcherViewModel)
            );

            Router.Navigate.Execute(accountSwitcherViewModel);
        }
    }
}
