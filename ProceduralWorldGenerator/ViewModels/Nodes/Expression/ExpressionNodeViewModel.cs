using System.Collections.ObjectModel;
using Nodify.Shared;
using ProceduralWorldGenerator.Validation;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Expression
{
    public class ExpressionNodeViewModel : NodeViewModelBase
    {
        private int _inputDimension;
        public ObservableCollection<Wrapper<string>> TransformExpressions { get; set; } = new ObservableCollection<Wrapper<string>>();
        
        public int OutputDimension
        {
            get => TransformExpressions.Count;
            set
            {
                TransformExpressions.Resize(value, "");
                OnPropertyChanged();
            }
        }

        public int InputDimension
        {
            get => _inputDimension;
            set => SetProperty(ref _inputDimension, value);
        }
    }
}