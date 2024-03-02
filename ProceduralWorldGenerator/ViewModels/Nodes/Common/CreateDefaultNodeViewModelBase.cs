namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public abstract class CreateDefaultNodeViewModelBase<TNodeViewModel> : CreateNodeMenuViewModelBase<TNodeViewModel> where TNodeViewModel : NodeViewModelBase
    {
        public override void Show()
        {
            CreateOperation.Execute(null);
        }
    }
}