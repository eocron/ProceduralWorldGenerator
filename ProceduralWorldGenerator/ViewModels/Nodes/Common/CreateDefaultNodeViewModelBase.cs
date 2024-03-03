namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public abstract class CreateDefaultNodeViewModelBase<TNodeViewModel> : CreateNodeMenuViewModelBase<TNodeViewModel> where TNodeViewModel : NodeViewModelBase
    {
        public override void ShowCreateDialog()
        {
            CreateOperation.Execute(null);
        }

        public override void ShowEditDialog()
        {
            Close();
        }
    }
}