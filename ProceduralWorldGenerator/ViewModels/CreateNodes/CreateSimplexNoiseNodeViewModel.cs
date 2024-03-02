using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public class CreateSimplexNoiseNodeViewModel : CreateDimensionNodeViewModelBase<SimplexNoiseNodeViewModel>
    {
        public CreateSimplexNoiseNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
            Description = "New Simplex noise";
        }
    }
}