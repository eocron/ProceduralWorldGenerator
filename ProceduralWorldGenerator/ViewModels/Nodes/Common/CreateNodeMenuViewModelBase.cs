namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public abstract class CreateNodeMenuViewModelBase<TNodeViewModel> : CreateMenuViewModelBase
        where TNodeViewModel : NodeViewModelBase
    {
        public TNodeViewModel NodeViewModel
        {
            get => (TNodeViewModel)Model;
            set
            {
                Model = value;
                OnPropertyChanged(nameof(NodeViewModel));
            }
        }
    }
}