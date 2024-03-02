using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.SimplexNoise
{
    public class CreateSimplexNoiseNodeViewModel : CreateDimensionNodeViewModelBase<SimplexNoiseNodeViewModel>
    {
        public CreateSimplexNoiseNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
            Description = "New Simplex noise";
        }
    }
}