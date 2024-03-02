namespace ProceduralWorldGenerator.ViewModels.Nodes.Permutation
{
    public class PermutationTableNodeViewModel : NodeViewModelBase
    {
        private PermutationTableParameterViewModel _permutation = new();

        public PermutationTableParameterViewModel Permutation
        {
            get => _permutation;
            set => SetProperty(ref _permutation, value);
        }
    }
}