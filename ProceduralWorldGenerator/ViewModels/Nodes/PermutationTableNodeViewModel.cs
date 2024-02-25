using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class PermutationTableNodeViewModel : NodeViewModelBase
    {
        private PermutationTableParameterViewModel _permutation = new PermutationTableParameterViewModel()
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
            Title = "Permutation table";
        }
    }
}