using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Output
{
    public class OutputVectorNodeViewModel : NodeViewModelBase
    {
        public VectorParameterViewModel Input { get; set; } = new VectorParameterViewModel()
        {
            IsInput = true
        };
        
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