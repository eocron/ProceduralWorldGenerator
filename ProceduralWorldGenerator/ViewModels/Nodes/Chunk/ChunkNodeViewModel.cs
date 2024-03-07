﻿using Newtonsoft.Json;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Chunk
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ChunkNodeViewModel : NodeViewModelBase
    {
        [JsonProperty]
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

        [JsonProperty]
        public VectorParameterViewModel Offset
        {
            get => _offset;
            set => SetProperty(ref _offset, value);
        }

        [JsonProperty]
        public VectorParameterViewModel Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        [JsonProperty]
        public VectorParameterViewModel Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        private VectorParameterViewModel _offset = new()
        {
            IsInput = true
        };

        private VectorParameterViewModel _position = new();

        private VectorParameterViewModel _size = new()
        {
            IsInput = true
        };
    }
}