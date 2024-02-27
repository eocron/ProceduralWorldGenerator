using ProceduralWorldGenerator.Validation;
using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public abstract class CreateDimensionNodeViewModelBase<TNodeViewModel> : CreateNodeViewModelBase<TNodeViewModel>
        where TNodeViewModel : NodeViewModelBase, IDimensionModel
    {
        public CreateDimensionNodeViewModelBase(GeneratorViewModel calculator) : base(calculator)
        {
        }
    }
}