using ReactiveUI;
using SteamAccountManager.AvaloniaUI.Common;
using SteamAccountManager.AvaloniaUI.ViewModels.Commands;
using System.Reactive;

namespace SteamAccountManager.AvaloniaUI.ViewModels
{
    public class NavigationTarget
    {
        public string Title { get; set; }
        public string HintText { get; set; }
        public QuickCommand NavigateCommand { get; set; }

        public NavigationTarget(string title, string hintText, QuickCommand navigateCommand)
        {
            Title = title;
            HintText = hintText;
            NavigateCommand = navigateCommand;
        }
    }

    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        // The Router associated with this Screen.
        // Required by the IScreen interface.
        public RoutingState Router { get; } = new RoutingState();

        public AdvancedObservableCollection<NavigationTarget> NavigationTargets { get; } = new();

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
            NavigationTargets.Add(
                new NavigationTarget
                (
                    title: "Accounts",
                    hintText: "Show Accounts",
                    new QuickCommand(() => NavigateTo(_viewModelStore.Get<AccountSwitcherViewModel>(this)))
                )
            );
            NavigationTargets.Add(
                new NavigationTarget
                (
                    title: "Settings",
                    hintText: "Show Settings",
                    new QuickCommand(() => NavigateTo(_viewModelStore.Get<SettingsViewModel>(this)))
                )
            );

            NavigateTo(_viewModelStore.Get<AccountSwitcherViewModel>(this));
        }

        private void NavigateTo(IRoutableViewModel viewModel)
        {
            if (IsViewAlreadyVisible(viewModel)) 
                return;

            Router.Navigate.Execute(viewModel);
        }

        public bool IsViewAlreadyVisible(IRoutableViewModel viewModel) => viewModel == Router.GetCurrentViewModel();
    }
}
