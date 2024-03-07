using System;

namespace ProceduralWorldGenerator.Common
{
    public class DelegateCommand : INodifyCommand
    {
        public DelegateCommand(Action action, Func<bool>? executeCondition = default)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _condition = executeCondition;
        }

        public bool CanExecute(object parameter)
        {
            return _condition?.Invoke() ?? true;
        }

        public event EventHandler? CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private readonly Action _action;
        private readonly Func<bool>? _condition;
    }

    public class DelegateCommand<T> : INodifyCommand
    {
        public DelegateCommand(Action<T> action, Func<T, bool>? executeCondition = default)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _condition = executeCondition;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is T value) return _condition?.Invoke(value) ?? true;

            return _condition?.Invoke(default!) ?? true;
        }

        public event EventHandler? CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (parameter is T value)
                _action(value);
            else
                _action(default!);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private readonly Action<T> _action;
        private readonly Func<T, bool>? _condition;
    }
}