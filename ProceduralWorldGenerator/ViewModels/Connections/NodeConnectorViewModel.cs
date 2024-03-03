using System.Windows;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Connections
{
    public class NodeConnectorViewModel : ObservableObject
    {
        private GeneratorNodeViewModel _operation = default!;
        private ParameterViewModelBase _parameterViewModel;
        private string? _title;
        private bool _isConnected;
        private bool _isInput;
        private Point _anchor;
        
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }
        
        public bool IsInput
        {
            get => _isInput;
            set => SetProperty(ref _isInput, value);
        }
        
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
