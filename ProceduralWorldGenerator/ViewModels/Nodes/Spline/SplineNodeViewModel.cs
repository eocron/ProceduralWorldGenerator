using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Spline
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

        private SplineEditorViewModel _spline = new SplineEditorViewModel();

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

        public SplineEditorViewModel Spline
        {
            get => _spline;
            set => SetProperty(ref _spline, value);
        }
    }
}