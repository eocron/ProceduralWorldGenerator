using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public class CreateVectorNodeViewModel : CreateDimensionNodeViewModelBase<VectorNodeViewModel>
    {
        public CreateVectorNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
        }
    }
}