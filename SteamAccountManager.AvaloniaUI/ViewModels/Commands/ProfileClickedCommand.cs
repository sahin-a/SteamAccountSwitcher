using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SteamAccountManager.AvaloniaUI.ViewModels.Commands
{
    internal class ProfileClickedCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return parameter is string && (parameter as string).Length > 0;
        }

        public void Execute(object? parameter)
        {
            if (parameter is string)
            {
                Process.Start("explorer", parameter as string);
            }
        }
    }
}
