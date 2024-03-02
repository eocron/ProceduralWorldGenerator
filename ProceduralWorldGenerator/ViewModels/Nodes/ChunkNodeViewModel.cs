﻿using ProceduralWorldGenerator.Validation;
using ProceduralWorldGenerator.ViewModels.Nodes.Parameters;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    public class ChunkNodeViewModel : NodeViewModelBase, IDimensionModel
    {
        private VectorParameterViewModel _offset = new()
        {
            Title = "v",
            IsInput = true
        };

        private VectorParameterViewModel _size = new()
        {
            IsInput = true,
            Title = "size"
        };

        private VectorParameterViewModel _position = new()
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

        public ChunkNodeViewModel()
        {
            VariableName = "chunk";
        }
    }
}