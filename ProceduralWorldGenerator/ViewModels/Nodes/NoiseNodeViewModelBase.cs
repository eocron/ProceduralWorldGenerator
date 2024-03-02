using ProceduralWorldGenerator.Validation;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Permutation;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class NoiseNodeViewModelBase : NodeViewModelBase, IDimensionModel
    {
        private PermutationTableParameterViewModel _permutation = new()
        {
            IsInput = true
        };

        private VectorParameterViewModel _input = new()
        {
            IsInput = true
        };

        private VectorParameterViewModel _output = new()
        {
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

        public int Dimension
        {
            get => Input.Dimension;
            set
            {
                Input.Dimension = value;
            }
        }

        public override string Title => Dimension != 0 ? $"{VariableName} {Dimension}D" : VariableName;
    }
}