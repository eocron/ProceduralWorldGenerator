namespace ProceduralWorldGenerator.ViewModels.Nodes.WorleyNoise
{
    public class WorleyNoiseNodeViewModel : NoiseNodeViewModelBase
    {
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

        public WorleyCombinationParameterViewModel Distance
        {
            get => _distance;
            set => SetProperty(ref _distance, value);
        }

        public WorleyDistanceParameterViewModel Combination
        {
            get => _combination;
            set => SetProperty(ref _combination, value);
        }

        public WorleyNoiseNodeViewModel()
        {
            VariableName = "Worley noise";
        }
    }
}