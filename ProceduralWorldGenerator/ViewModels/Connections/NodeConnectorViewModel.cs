using System.Windows;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels.Connections
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NodeConnectorViewModel : ObservableObject
    {
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

        [JsonProperty]
        public string NodeId
        {
            get => _nodeId;
            set => SetProperty(ref _nodeId, value);
        }

        [JsonProperty]
        public string NodeParameterId
        {
            get => _nodeParameterId;
            set => SetProperty(ref _nodeParameterId, value);
        }

        [JsonProperty]
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isConnected;
        private bool _isInput;
        private GeneratorNodeViewModel _operation = default!;
        private Point _anchor;
        private string _nodeId;
        private string _nodeParameterId;
        private string? _title;
    }
}