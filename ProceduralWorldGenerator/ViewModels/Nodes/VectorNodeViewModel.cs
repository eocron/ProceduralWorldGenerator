using System.Collections.Generic;
using ProceduralWorldGenerator.Validation;
using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class VectorNodeViewModel : NodeViewModelBase, IDimensionSetter
    {
        private VectorParameterViewModel _value = new VectorParameterViewModel()
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
            Title = "vector";
        }

        public void SetDimension(int dimension)
        {
            Value.Dimension = dimension;
            IsDirty = true;
            OnPropertyChanged(nameof(Value));
        }

        public int MinDimension => Value.MinDimension;
        public int MaxDimension => Value.MaxDimension;
        public IReadOnlySet<int> AllowedDimensions => Value.AllowedDimensions;
    }
}