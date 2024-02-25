using System.Collections.Generic;
using ProceduralWorldGenerator.Validation;
using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class ChunkNodeViewModel : NodeViewModelBase, IDimensionSetter
    {
        private VectorParameterViewModel _offset = new VectorParameterViewModel()
        {
            Title = "v",
            IsInput = true
        };

        private VectorParameterViewModel _size = new VectorParameterViewModel()
        {
            MinDimension = 1,
            MaxDimension = 100,
            IsInput = true,
            Title = "size"
        };

        private VectorParameterViewModel _position = new VectorParameterViewModel()
        {
            Title = "v"
        };

        public VectorParameterViewModel Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        public VectorParameterViewModel Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        public VectorParameterViewModel Offset
        {
            get => _offset;
            set => SetProperty(ref _offset, value);
        }

        public int MinDimension => Size.MinDimension;
        public int MaxDimension => Size.MaxDimension;
        public IReadOnlySet<int> AllowedDimensions => Size.AllowedDimensions;

        public ChunkNodeViewModel()
        {
            Title = "chunk";
        }
        public void SetDimension(int dimension)
        {
            Size.Dimension = dimension;
            Position.Dimension = dimension;
            Offset.Dimension = dimension;
            IsDirty = true;
            OnPropertyChanged(nameof(Size));
            OnPropertyChanged(nameof(Position));
            OnPropertyChanged(nameof(Offset));
        }
    }
}