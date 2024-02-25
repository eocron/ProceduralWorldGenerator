using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class WorleyNoiseNodeViewModel : NoiseNodeViewModelBase
    {
        private WorleyCombinationParameterViewModel _distance = new WorleyCombinationParameterViewModel()
        {
            Title = "combination",
            IsInput = true
        };

        private WorleyDistanceParameterViewModel _combination = new WorleyDistanceParameterViewModel()
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
            Title = "Worley noise";
        }
    }
}