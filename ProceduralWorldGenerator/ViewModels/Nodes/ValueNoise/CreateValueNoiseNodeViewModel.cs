﻿using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.ValueNoise
{
    public class CreateValueNoiseNodeViewModel : CreateDimensionNodeViewModelBase<ValueNoiseNodeViewModel>
    {
        public CreateValueNoiseNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
        }
    }
}