using System;
using System.Windows.Input;

namespace ProceduralWorldGenerator.Common
{
    public class RequeryCommand : INodifyCommand
    {
        public RequeryCommand(Action action, Func<bool>? executeCondition = default)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _condition = executeCondition;
        }

        public bool CanExecute(object parameter)
        {
            return _condition?.Invoke() ?? true;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public void RaiseCanExecuteChanged()
        {
        }

        private readonly Action _action;
        private readonly Func<bool>? _condition;
    }

    public class RequeryCommand<T> : INodifyCommand
    {
        public RequeryCommand(Action<T> action, Func<T, bool>? executeCondition = default)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _condition = executeCondition;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is T value) return _condition?.Invoke(value) ?? true;

            return _condition?.Invoke(default!) ?? true;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            if (parameter is T value)
                _action(value);
            else
                _action(default!);
        }

        public void RaiseCanExecuteChanged()
        {
        }

        private readonly Action<T> _action;
        private readonly Func<T, bool>? _condition;
    }
}