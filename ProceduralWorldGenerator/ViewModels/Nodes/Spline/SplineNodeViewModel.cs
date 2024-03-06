using Newtonsoft.Json;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Spline
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SplineNodeViewModel : NodeViewModelBase
    {
        private VectorParameterViewModel _input = new VectorParameterViewModel()
        {
            Dimension = 1,
            IsInput = true,
        };
        private VectorParameterViewModel _output = new VectorParameterViewModel()
        {
            Dimension = 1,
        };

        private SplineEditorViewModel _spline = new SplineEditorViewModel();

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
        [JsonProperty]
        public SplineEditorViewModel Spline
        {
            get => _spline;
            set => SetProperty(ref _spline, value);
        }
    }
}