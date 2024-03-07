using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels.Connections
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NodeConnectionViewModel : ObservableObject
    {
        [JsonProperty]
        public NodeConnectorViewModel Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        [JsonProperty]
        public NodeConnectorViewModel Output
        {
            get => _output;
            set => SetProperty(ref _output, value);
        }

        private NodeConnectorViewModel _input = default!;

        private NodeConnectorViewModel _output = default!;
    }
}