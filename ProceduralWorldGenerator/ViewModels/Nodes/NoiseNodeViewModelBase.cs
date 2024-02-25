using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public abstract class NoiseNodeViewModelBase : NodeViewModelBase
    {
        private PermutationTableParameterViewModel _permutation = new PermutationTableParameterViewModel()
        {
            Title = "rnd",
            IsInput = true
        };

        private VectorParameterViewModel _input = new VectorParameterViewModel()
        {
            Title = "v",
            MinDimension = 1, 
            MaxDimension = 3, 
            IsInput = true
        };

        private VectorParameterViewModel _output = new VectorParameterViewModel()
        {
            Title = "v",
            MinDimension = 1, 
            MaxDimension = 1, 
            Dimension = 1
        };

        public PermutationTableParameterViewModel Permutation
        {
            get => _permutation;
            set => SetProperty(ref _permutation, value);
        }

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
    }
}