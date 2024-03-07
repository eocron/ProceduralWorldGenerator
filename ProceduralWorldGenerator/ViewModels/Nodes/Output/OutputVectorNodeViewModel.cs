using Newtonsoft.Json;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Output
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OutputVectorNodeViewModel : NodeViewModelBase
    {
        [JsonProperty]
        public int Dimension
        {
            get => Input.Dimension;
            set { SetNestedProperty(nameof(Input), Input.Dimension, value, () => Input.Dimension = value); }
        }

        public override string Title => Dimension != 0 ? $"{VariableName} {Dimension}D" : VariableName;

        [JsonProperty]
        public VectorParameterViewModel Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        private VectorParameterViewModel _input = new()
        {
            IsInput = true
        };
    }
}