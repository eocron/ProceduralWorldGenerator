using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class NodeViewModelBase : ObservableObject
    {
        private string _variableName;

        public virtual string Title => _variableName;

        public string VariableName
        {
            get => _variableName;
            set => SetProperty(ref _variableName, value);
        }
    }
}