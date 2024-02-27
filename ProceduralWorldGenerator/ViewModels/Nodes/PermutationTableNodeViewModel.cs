using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class PermutationTableNodeViewModel : NodeViewModelBase
    {
        private PermutationTableParameterViewModel _permutation = new()
        {
            Title = "rnd"
        };

        public PermutationTableParameterViewModel Permutation
        {
            get => _permutation;
            set => SetProperty(ref _permutation, value);
        }

        public PermutationTableNodeViewModel()
        {
            VariableName = "Permutation table";
        }
    }
}