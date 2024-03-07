using Newtonsoft.Json;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Spline
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SplineNodeViewModel : NodeViewModelBase
    {
        [JsonProperty]
        public SplineEditorViewModel Spline
        {
            get => _spline;
            set => SetProperty(ref _spline, value);
        }

        [JsonProperty]
        public VectorParameterViewModel Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        [JsonProperty]
        public VectorParameterViewModel Output
        {
            get => _output;
            set => SetProperty(ref _output, value);
        }

        private SplineEditorViewModel _spline = new();

        private VectorParameterViewModel _input = new()
        {
            Dimension = 1,
            IsInput = true
        };

        private VectorParameterViewModel _output = new()
        {
            Dimension = 1
        };
    }
}