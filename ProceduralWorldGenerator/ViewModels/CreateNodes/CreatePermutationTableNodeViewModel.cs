using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public class CreatePermutationTableNodeViewModel : CreateDefaultNodeViewModelBase<PermutationTableNodeViewModel>
    {
        public CreatePermutationTableNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
        }
    }
}