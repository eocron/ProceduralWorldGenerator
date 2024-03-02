using System.Collections.ObjectModel;
using Nodify.Shared;
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

        private bool _isConstant;

        public VectorParameterViewModel Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public VectorNodeViewModel()
        {
            VariableName = "vector";
        }

        public int Dimension
        {
            get => Value.Dimension;
            set
            {
                var diff = Values.Count - value;
                if (diff > 0)
                {
                    for (int i = 0; i < diff; i++)
                    {
                        Values.RemoveAt(Values.Count-1);
                    }
                }
                else
                {
                    for (int i = 0; i < -diff; i++)
                    {
                        Values.Add(0);
                    }
                }
                SetNestedProperty(nameof(Value), Value.Dimension, value, () => Value.Dimension = value);
            }
        }

        public bool IsConstant
        {
            get => _isConstant;
            set => SetProperty(ref _isConstant, value);
        }

        public ObservableCollection<Wrapper<float>> Values { get; set; } = new ObservableCollection<Wrapper<float>>();

        public override string Title => Dimension != 0 ? $"{VariableName} {Dimension}D" : VariableName;
    }
}