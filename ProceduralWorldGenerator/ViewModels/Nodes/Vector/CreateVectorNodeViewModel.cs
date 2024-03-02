using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Vector
{
    public class CreateVectorNodeViewModel : CreateDimensionNodeViewModelBase<VectorNodeViewModel>
    {
        public CreateVectorNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
            Description = "New vector";
        }
    }
}