using System.ComponentModel;
using System.Windows;
using Nodify.Shared;
using ProceduralWorldGenerator.ViewModels.Connections;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Control
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
        private NodeViewModelBase _nodeModel;

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public NodeViewModelBase NodeModel
        {
            get => _nodeModel;
            set => SetProperty(ref _nodeModel, value);
        }

        public NodifyObservableCollection<ConnectorViewModel> Input { get; } = new NodifyObservableCollection<ConnectorViewModel>();
        
        public NodifyObservableCollection<ConnectorViewModel> Output { get; } = new NodifyObservableCollection<ConnectorViewModel>();
    }
}
