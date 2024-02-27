using ProceduralWorldGenerator.Validation;
using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class VectorNodeViewModel : NodeViewModelBase, IDimensionModel
    {
        private VectorParameterViewModel _value = new()
        {
            Title = "v"
        };

        public VectorParameterViewModel Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public VectorNodeViewModel()
        {
            VariableName = "vector";
        }

        public int MinDimension => Value.MinDimension;
        public int MaxDimension => Value.MaxDimension;

        public int Dimension
        {
            get => Value.Dimension;
            set
            {
                SetNestedProperty(nameof(Value), Value.Dimension, value, () => Value.Dimension = value);
            }
        }
        
        public override string Title => Dimension != 0 ? $"{VariableName} {Dimension}D" : VariableName;
    }
}