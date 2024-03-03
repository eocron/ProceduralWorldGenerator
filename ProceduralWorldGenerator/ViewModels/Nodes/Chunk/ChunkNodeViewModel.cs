using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Chunk
{
    public class ChunkNodeViewModel : NodeViewModelBase
    {
        private VectorParameterViewModel _offset = new()
        {
            IsInput = true
        };

        private VectorParameterViewModel _size = new()
        {
            IsInput = true,
        };

        private VectorParameterViewModel _position = new();

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

        public int Dimension
        {
            get => Size.Dimension;
            set
            {
                SetNestedProperty(nameof(Position), Position.Dimension, value, () => Position.Dimension = value);
                SetNestedProperty(nameof(Size), Size.Dimension, value, () => Size.Dimension = value);
                SetNestedProperty(nameof(Offset), Offset.Dimension, value, () => Offset.Dimension = value);
            }
        }
    }
}