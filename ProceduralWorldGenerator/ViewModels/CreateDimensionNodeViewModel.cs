using System;
using System.Collections.Generic;
using ProceduralWorldGenerator.Validation;
using ProceduralWorldGenerator.ViewModels.Nodes.Control;

namespace ProceduralWorldGenerator.ViewModels
{
    public class CreateDimensionNodeViewModel : CreateNodeViewModelBase, IDimensionValidationInfo
    {
        private readonly Func<CreateDimensionNodeViewModel, OperationViewModel> _onCreate;
        private int _minDimension = 1;
        private int _maxDimension = int.MaxValue;
        private IReadOnlySet<int> _availableDimensions;
        private int _dimension = 1;
        private string _textBox = "1";
        private string _description = "Select dimension";

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

        public string TextBox
        {
            get => _textBox;
            set => SetProperty(ref _textBox, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        
        public Func<OperationViewModel> OperationViewModelProvider { get; set; }

        public CreateDimensionNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
        }

        protected override OperationViewModel Create()
        {
            return OperationViewModelProvider();
        }
    }
}