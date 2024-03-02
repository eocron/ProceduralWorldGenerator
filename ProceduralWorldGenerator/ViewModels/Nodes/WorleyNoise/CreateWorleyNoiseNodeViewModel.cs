using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.WorleyNoise
{
    public class CreateWorleyNoiseNodeViewModel : CreateDimensionNodeViewModelBase<WorleyNoiseNodeViewModel>
    {
        public CreateWorleyNoiseNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
            Description = "New Worley noise";
        }
    }
}