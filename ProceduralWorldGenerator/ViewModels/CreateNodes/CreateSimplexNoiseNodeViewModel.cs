using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public class CreateSimplexNoiseNodeViewModel : CreateDimensionNodeViewModelBase<ValueNoiseNodeViewModel>
    {
        public CreateSimplexNoiseNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
        }
    }
}