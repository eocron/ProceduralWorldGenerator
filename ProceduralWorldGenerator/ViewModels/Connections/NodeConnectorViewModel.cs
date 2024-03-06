using System.Windows;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Connections
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NodeConnectorViewModel : ObservableObject
    {
        private GeneratorNodeViewModel _operation = default!;
        private ParameterViewModelBase _parameterViewModel;
        private string? _title;
        private bool _isConnected;
        private bool _isInput;
        private Point _anchor;
        
        [JsonProperty]
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        [JsonProperty]
        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }
        
        [JsonProperty]
        public bool IsInput
        {
            get => _isInput;
            set => SetProperty(ref _isInput, value);
        }
        
        [JsonProperty]
        public Point Anchor
        {
            get => _anchor;
            set => SetProperty(ref _anchor, value);
        }
        
        public GeneratorNodeViewModel Operation
        {
            get => _operation;
            set => SetProperty(ref _operation, value);
        }

        [JsonProperty]
        public ParameterViewModelBase ParameterViewModel
        {
            get => _parameterViewModel;
            set => SetProperty(ref _parameterViewModel, value);
        }

        public bool CanConnect(NodeConnectorViewModel other)
        {
            return ParameterViewModel.CanConnect(other.ParameterViewModel);
        }
    }
}
