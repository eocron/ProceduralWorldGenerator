using System.Windows;
using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public abstract class CreateDefaultNodeViewModelBase<TNodeViewModel> : CreateNodeViewModelBase<TNodeViewModel> where TNodeViewModel : NodeViewModelBase
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