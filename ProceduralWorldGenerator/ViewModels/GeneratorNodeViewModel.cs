using System;
using System.Windows;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.ViewModels.Connections;
using ProceduralWorldGenerator.ViewModels.Nodes;
using ProceduralWorldGenerator.ViewModels.Nodes.Expression;

namespace ProceduralWorldGenerator.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GeneratorNodeViewModel : ObservableObject
    {
        public GeneratorNodeViewModel()
        {
            Input.WhenAdded(x =>
            {
                x.IsInput = true;
            });
            Output.WhenAdded(x =>
            {
                x.IsInput = false;
            });
        }

        private Point _location;
        
        [JsonProperty]
        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private Size _size;
        
        [JsonProperty]
        public Size Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        public string? Title => _nodeModel.Title;

        private bool _isSelected;
        private NodeViewModelBase _nodeModel;

        [JsonProperty]
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        [JsonProperty]
        public NodeViewModelBase NodeModel
        {
            get => _nodeModel;
            set => SetProperty(ref _nodeModel, value);
        }

        [JsonProperty]
        public NodifyObservableCollection<NodeConnectorViewModel> Input { get; } = new();
        [JsonProperty]
        public NodifyObservableCollection<NodeConnectorViewModel> Output { get; } = new();
    }
}
