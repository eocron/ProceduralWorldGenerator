using ProceduralWorldGenerator.Validation;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public class VectorParameterViewModel : ParameterViewModelBase<float[]>, IDimensionModel
    {
        private int _dimension;

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