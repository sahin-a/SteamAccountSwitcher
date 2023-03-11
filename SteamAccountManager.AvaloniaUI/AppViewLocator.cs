using ReactiveUI;
using SteamAccountManager.AvaloniaUI.ViewModels;
using SteamAccountManager.AvaloniaUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.AvaloniaUI
{
    public class AppViewLocator : IViewLocator
    {
        public IViewFor? ResolveView<T>(T viewModel, string? contract = null)
        {
            string? viewPath = viewModel?.GetType().FullName?.Replace("ViewModels", "Views").Replace("ViewModel", "View");

            switch (viewPath)
            {
                case not null:
                    return createInstanceFromPath(viewPath);
                default:
                    return null;
            }
        }

        private IViewFor createInstanceFromPath(string path)
        {
            Type classType = Type.GetType(path, true) ?? throw new Exception("Couldn't resolve type from path");
            return Activator.CreateInstance(classType) as IViewFor ?? throw new Exception("Type isn't of type IViewFor!");
        }
    }
}
