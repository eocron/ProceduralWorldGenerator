using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Vector
{
    [JsonObject(MemberSerialization.OptIn)]
    public class VectorNodeViewModel : NodeViewModelBase
    {
        [JsonProperty]
        public bool IsConstant
        {
            get => _isConstant;
            set => SetProperty(ref _isConstant, value);
        }

        [JsonProperty]
        public int Dimension
        {
            get => Value.Dimension;
            set
            {
                Values.Resize(value, _ => 0);
                SetNestedProperty(nameof(Value), Value.Dimension, value, () => Value.Dimension = value);
            }
        }

        [JsonProperty] public ObservableCollection<Wrapper<float>> Values { get; set; } = new();

        public override string Title => Dimension != 0 ? $"{VariableName} {Dimension}D" : VariableName;

        [JsonProperty]
        public VectorParameterViewModel Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private bool _isConstant;

        private VectorParameterViewModel _value = new()
        {
            Dimension = 1
        };
    }
}