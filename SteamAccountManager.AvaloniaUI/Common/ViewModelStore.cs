using System;
using System.Collections.Generic;
using Autofac;
using ReactiveUI;
using SteamAccountManager.Domain.Common.CodeExtensions;

namespace SteamAccountManager.AvaloniaUI.Common
{
    internal class ViewModelStore
    {
        private readonly Dictionary<Type, IRoutableViewModel> _viewModels = new Dictionary<Type, IRoutableViewModel>();

        public IRoutableViewModel Get<T>(IScreen screen) where T : class, IRoutableViewModel
        {
            var viewModel = _viewModels.GetOrNull(typeof(T));
            if (viewModel is null)
            {
                viewModel = CreateViewModel<T>(screen);
                Register(viewModel);
            }

            return viewModel;
        }

        public void Register(IRoutableViewModel viewModel) => _viewModels.Add(viewModel.GetType(), viewModel);

        private IRoutableViewModel CreateViewModel<T>(IScreen screen) where T : class, IRoutableViewModel
            => Dependencies.Container?.Resolve<T>(new TypedParameter(typeof(IScreen), screen)) ??
               throw new Exception("Failed to resolve AccountSwitcherViewModel");
    }
}