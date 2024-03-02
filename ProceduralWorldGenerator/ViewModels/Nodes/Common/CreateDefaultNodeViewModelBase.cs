using System.Windows;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public abstract class CreateDefaultNodeViewModelBase<TNodeViewModel> : CreateMenuViewModelBase<TNodeViewModel> where TNodeViewModel : NodeViewModelBase
    {
        public CreateDefaultNodeViewModelBase(GeneratorViewModel calculator) : base(calculator)
        {
        }

        public override void OpenAt(Point targetLocation)
        {
            base.OpenAt(targetLocation);
            OnCreateOperation();
            Close();
        }
    }
}