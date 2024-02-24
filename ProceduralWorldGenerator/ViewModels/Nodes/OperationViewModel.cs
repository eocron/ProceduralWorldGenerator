using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Nodify.Shared;
using ProceduralWorldGenerator.Operations;
using ProceduralWorldGenerator.ViewModels.Connections;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class OperationViewModel : ObservableObject
    {
        public OperationViewModel()
        {
            Input.WhenAdded(x =>
            {
                x.Operation = this;
                x.IsInput = true;
                x.PropertyChanged += OnInputValueChanged;
            })
            .WhenRemoved(x =>
            {
                x.PropertyChanged -= OnInputValueChanged;
            });
            Output.WhenAdded(x =>
            {
                x.Operation = this;
                x.IsInput = false;
            });
        }

        private void OnInputValueChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        private Point _location;
        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private Size _size;
        public Size Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public bool IsReadOnly { get; set; }

        private bool _isRuntimeInput;
        
        public bool IsRuntimeInput         {
            get => _isRuntimeInput;
            set => SetProperty(ref _isRuntimeInput, value);
        }

        private IOperation? _operation;
        public IOperation? Operation
        {
            get => _operation;
            set => SetProperty(ref _operation, value)
                .Then(OnInputValueChanged);
        }

        public NodifyObservableCollection<ConnectorViewModel> Input { get; } = new NodifyObservableCollection<ConnectorViewModel>();
        
        public NodifyObservableCollection<ConnectorViewModel> Output { get; } = new NodifyObservableCollection<ConnectorViewModel>();

        protected virtual void OnInputValueChanged()
        {
            if (Operation != null)
            {
                try
                {
                    //var input = Input.Select(i => i.Value).ToArray();
                    //Output.Value = Operation?.Execute(input) ?? 0;
                }
                catch(Exception ex)
                {
                    Trace.WriteLine(ex);
                }
            }
        }
    }
}
