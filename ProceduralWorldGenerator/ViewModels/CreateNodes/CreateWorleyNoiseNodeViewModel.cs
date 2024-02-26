using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public class CreateWorleyNoiseNodeViewModel : CreateDimensionNodeViewModelBase<WorleyNoiseNodeViewModel>
    {
        public CreateWorleyNoiseNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
        }
    }
}