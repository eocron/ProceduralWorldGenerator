using System.Windows;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Connections
{
    public class NodeConnectorViewModel : ObservableObject
    {
        private string? _variableTitle;
        public string? VariableTitle
        {
            get => _variableTitle;
            set
            {
                SetProperty(ref _variableTitle, value);
                OnPropertyChanged(nameof(Title));
            }
        }

        private string? _title;
        public string? Title
        {
            get => _variableTitle ?? _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        private bool _isInput;
        public bool IsInput
        {
            get => _isInput;
            set => SetProperty(ref _isInput, value);
        }

        private Point _anchor;
        public Point Anchor
        {
            get => _anchor;
            set => SetProperty(ref _anchor, value);
        }

        private GeneratorNodeViewModel _operation = default!;
        private ParameterViewModelBase _parameterViewModel;

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

        public void SetTitleFrom(NodeConnectorViewModel output)
        {
            VariableTitle = output.Title;
        }

        public bool CanConnect(NodeConnectorViewModel other)
        {
            return ParameterViewModel.CanConnect(other.ParameterViewModel);
        }

        public void RestoreTitle()
        {
            VariableTitle = null;
        }
    }
}
