using System;
using System.Windows.Input;

namespace SteamAccountManager.AvaloniaUI.ViewModels.Commands
{
    public class QuickCommand : ICommand
    {
        private readonly Action _func;

        public QuickCommand(Action func)
        {
            _func = func;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _func();
        }
    }

    internal class QuickCommand<T> : ICommand
    {
        private readonly Action<T> _func;

        public QuickCommand(Action<T> func)
        {
            _func = func;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is T param)
            {
                _func(param);
            }
        }
    }
}
