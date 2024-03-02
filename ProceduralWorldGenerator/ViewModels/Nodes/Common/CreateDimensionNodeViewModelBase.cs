using ProceduralWorldGenerator.Validation;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public abstract class CreateDimensionNodeViewModelBase<TNodeViewModel> : CreateMenuViewModelBase<TNodeViewModel>
        where TNodeViewModel : NodeViewModelBase, IDimensionModel
    {
    }
}