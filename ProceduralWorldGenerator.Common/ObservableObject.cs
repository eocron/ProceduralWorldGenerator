using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ProceduralWorldGenerator.Common
{
    public class ObservableObject : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _isDirty;
        public bool IsDirty
        {
            get => _isDirty;
            protected set
            {
                if (_isDirty != value)
                {
                    _isDirty = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool SetProperty<T>(ref T reference, T value, [CallerMemberName] in string propertyName = default!)
        {
            if (!Equals(reference, value))
            {
                reference = value;
                IsDirty = true;
                OnPropertyChanged(propertyName);
                return true;
            }

            return false;
        }

        protected void SetNestedProperty<T>(string propertyName, T oldValue, T newValue, Action setter)
        {
            if (!Equals(oldValue, newValue))
            {
                setter();
                IsDirty = true;
                OnPropertyChanged(propertyName);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] in string? propertyName = default)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
