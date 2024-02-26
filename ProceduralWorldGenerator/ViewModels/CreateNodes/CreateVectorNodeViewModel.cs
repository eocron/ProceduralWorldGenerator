using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public class CreateVectorNodeViewModel : CreateNodeViewModelBase<VectorNodeViewModel>
    {
        public CreateVectorNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
            Description = "New vector";
        }
    }
}