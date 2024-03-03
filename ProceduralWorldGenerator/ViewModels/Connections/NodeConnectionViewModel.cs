using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels.Connections
{
    public class NodeConnectionViewModel : ObservableObject
    {
        private NodeConnectorViewModel _input = default!;
        public NodeConnectorViewModel Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        private NodeConnectorViewModel _output = default!;
        public NodeConnectorViewModel Output
        {
            get => _output;
            set => SetProperty(ref _output, value);
        }
    }
}
