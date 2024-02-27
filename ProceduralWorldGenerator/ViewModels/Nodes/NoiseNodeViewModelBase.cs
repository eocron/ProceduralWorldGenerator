using ProceduralWorldGenerator.Validation;
using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class NoiseNodeViewModelBase : NodeViewModelBase, IDimensionModel
    {
        private PermutationTableParameterViewModel _permutation = new()
        {
            Title = "rnd",
            IsInput = true
        };

        private VectorParameterViewModel _input = new()
        {
            Title = "v",
            MinDimension = 1, 
            MaxDimension = 3, 
            IsInput = true
        };

        private VectorParameterViewModel _output = new()
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
        
        public int MinDimension => Input.MinDimension;
        public int MaxDimension => Input.MaxDimension;

        public int Dimension
        {
            get => Input.Dimension;
            set
            {
                SetNestedProperty(nameof(Input), Input.Dimension, value, () => Input.Dimension = value);
            }
        }

        public override string Title => Dimension != 0 ? $"{VariableName} {Dimension}D" : VariableName;
    }
}