using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class SplineNodeViewModel : NodeViewModelBase
    {
        private VectorParameterViewModel _input = new VectorParameterViewModel()
        {
            Dimension = 1,
            IsInput = true,
            Title = "v"
        };
        private VectorParameterViewModel _output = new VectorParameterViewModel()
        {
            Dimension = 1,
            Title = "v"
        };

        public VectorParameterViewModel Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        public VectorParameterViewModel Output
        {
            get => _output;
            set => SetProperty(ref _output, value);
        }

        public SplineNodeViewModel()
        {
            VariableName = "Spline";
        }
    }
}