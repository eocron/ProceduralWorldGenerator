using Newtonsoft.Json;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Permutation;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NoiseNodeViewModelBase : NodeViewModelBase
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

        [JsonProperty]
        public PermutationTableParameterViewModel Permutation
        {
            get => _permutation;
            set => SetProperty(ref _permutation, value);
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

        [JsonProperty]
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