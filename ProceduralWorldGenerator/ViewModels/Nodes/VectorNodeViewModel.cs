using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class VectorNodeViewModel : NodeViewModelBase
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
    }
}