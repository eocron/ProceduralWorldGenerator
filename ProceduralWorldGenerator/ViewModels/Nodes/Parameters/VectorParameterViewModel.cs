using ProceduralWorldGenerator.Validation;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Parameters
{
    public class VectorParameterViewModel : ParameterViewModelBase<float[]>, IDimensionModel
    {
        private int _minDimension = 1;
        private int _maxDimension = int.MaxValue;
        private int _dimension;

        public int MinDimension
        {
            get => _minDimension;
            set => SetProperty(ref _minDimension, value);
        }

        public int MaxDimension
        {
            get => _maxDimension;
            set => SetProperty(ref _maxDimension, value);
        }

        public int Dimension
        {
            get => _dimension;
            set => SetProperty(ref _dimension, value);
        }

        public override bool CanConnect(ParameterViewModelBase other)
        {
            var otherVector = other as VectorParameterViewModel;
            if (otherVector == null)
                return false;
            
            return Dimension == otherVector.Dimension;
        }
    }
}