using ProceduralWorldGenerator.Validation;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public abstract class CreateDimensionNodeViewModelBase<TNodeViewModel> : CreateNodeMenuViewModelBase<TNodeViewModel>
        where TNodeViewModel : NodeViewModelBase, IDimensionModel
    {
    }
}