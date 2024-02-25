using System.Collections.Generic;
using ProceduralWorldGenerator.Validation;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Parameters
{
    public class VectorParameterViewModel : ParameterViewModelBase<float[]>, IDimensionValidationInfo
    {
        private int _minDimension = 1;
        private int _maxDimension = int.MaxValue;
        private IReadOnlySet<int> _availableDimensions;
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

        public IReadOnlySet<int> AllowedDimensions
        {
            get => _availableDimensions;
            set => SetProperty(ref _availableDimensions, value);
        }

        public int Dimension
        {
            get => _dimension;
            set => SetProperty(ref _dimension, value);
        }
    }
}