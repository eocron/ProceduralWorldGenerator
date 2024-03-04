using System;
using System.Windows;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.ViewModels.Connections;
using ProceduralWorldGenerator.ViewModels.Nodes;
using ProceduralWorldGenerator.ViewModels.Nodes.Expression;

namespace ProceduralWorldGenerator.ViewModels
{
    public class GeneratorNodeViewModel : ObservableObject
    {
        public GeneratorNodeViewModel()
        {
            Input.WhenAdded(x =>
            {
                x.Operation = this;
                x.IsInput = true;
            });
            Output.WhenAdded(x =>
            {
                x.Operation = this;
                x.IsInput = false;
            });
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

        public string? Title => _nodeModel.Title;

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

        public NodifyObservableCollection<NodeConnectorViewModel> Input { get; } = new();
        
        public NodifyObservableCollection<NodeConnectorViewModel> Output { get; } = new();
    }
}
