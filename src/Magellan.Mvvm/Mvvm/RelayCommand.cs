using System;
using System.Diagnostics;
using System.Windows.Input;
using Magellan.Utilities;

namespace Magellan.Mvvm
{
    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action execute)
            : base(x => execute())
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
            : base(x => execute(), x => canExecute())
        {
        }
    }

    public class RelayCommand<TArgument> : ICommand
    {
        private readonly Action<TArgument> _execute;
        private readonly Func<TArgument, bool> _canExecute;
        
        public RelayCommand(Action<TArgument> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<TArgument> execute, Func<TArgument, bool> canExecute)
        {
            Guard.ArgumentNotNull(execute, "execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            if (parameter == null && typeof(TArgument).IsValueType)
                return false;
            
            return _canExecute == null ? true : _canExecute((TArgument)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        [DebuggerStepThrough]
        public void Execute(object parameter)
        {
            if (parameter == null && typeof(TArgument).IsValueType)
                return;

            _execute((TArgument)parameter);
        }
    }
}