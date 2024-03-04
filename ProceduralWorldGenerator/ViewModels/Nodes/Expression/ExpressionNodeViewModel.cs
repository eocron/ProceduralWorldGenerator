using System.Collections.ObjectModel;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Expression
{
    public class ExpressionNodeViewModel : NodeViewModelBase
    {
        public ObservableCollection<Wrapper<string>> TransformExpressions { get; set; } = new ObservableCollection<Wrapper<string>>();

        public VectorParameterViewModel Input { get; set; } = new VectorParameterViewModel(){IsInput = true};

        public VectorParameterViewModel Output { get; set; } = new VectorParameterViewModel();
        
        public int OutputDimension
        {
            get => Output.Dimension;
            set
            {
                TransformExpressions.Resize(value, _=>"");
                SetNestedProperty(nameof(Output), Output.Dimension, value, () => Output.Dimension = value);
                OnPropertyChanged();
            }
        }

        public int InputDimension
        {
            get => Input.Dimension;
            set
            {
                SetNestedProperty(nameof(Input), Input.Dimension, value, () => Input.Dimension = value);
                OnPropertyChanged();
            }
        }
    }
}