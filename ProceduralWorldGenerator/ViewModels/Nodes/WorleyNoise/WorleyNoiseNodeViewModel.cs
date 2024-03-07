using Newtonsoft.Json;

namespace ProceduralWorldGenerator.ViewModels.Nodes.WorleyNoise
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WorleyNoiseNodeViewModel : NoiseNodeViewModelBase
    {
        [JsonProperty]
        public WorleyCombinationParameterViewModel Distance
        {
            get => _distance;
            set => SetProperty(ref _distance, value);
        }

        [JsonProperty]
        public WorleyDistanceParameterViewModel Combination
        {
            get => _combination;
            set => SetProperty(ref _combination, value);
        }

        private WorleyCombinationParameterViewModel _distance = new()
        {
            Title = "combination",
            IsInput = true
        };

        private WorleyDistanceParameterViewModel _combination = new()
        {
            Title = "distance",
            IsInput = true
        };
    }
}