using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Permutation
{
    public class CreatePermutationTableNodeViewModel : CreateDefaultNodeViewModelBase<PermutationTableNodeViewModel>
    {
        public CreatePermutationTableNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
        }
    }
}