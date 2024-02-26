using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public class CreateValueNoiseNodeViewModel : CreateDimensionNodeViewModelBase<ValueNoiseNodeViewModel>
    {
        public CreateValueNoiseNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
        }
    }
}