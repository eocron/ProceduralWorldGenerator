using Newtonsoft.Json;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Permutation
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PermutationTableNodeViewModel : NodeViewModelBase
    {
        private PermutationTableParameterViewModel _permutation = new();
        [JsonProperty]
        public PermutationTableParameterViewModel Permutation
        {
            get => _permutation;
            set => SetProperty(ref _permutation, value);
        }
    }
}