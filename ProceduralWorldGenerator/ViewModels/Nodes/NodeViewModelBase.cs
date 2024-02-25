using Nodify.Shared;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class NodeViewModelBase : ObservableObject
    {
        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}