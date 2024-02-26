using System.Collections.Generic;
using ProceduralWorldGenerator.Validation;
using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public abstract class CreateDimensionNodeViewModelBase<TNodeViewModel> : CreateNodeViewModelBase<TNodeViewModel>, IDimensionValidationInfo
        where TNodeViewModel : NodeViewModelBase, IDimensionSetter
    {
        private int _minDimension = 1;
        private int _maxDimension = int.MaxValue;
        private IReadOnlySet<int> _availableDimensions;
        private int? _dimension = null;
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

        public int? Dimension
        {
            get => _dimension;
            set => SetProperty(ref _dimension, value);
        }

        public string TextBox
        {
            get => _textBox;
            set
            {
                SetProperty(ref _textBox, value);
                if (ValidationHelper.IsDimensionTextAllowed(value, this))
                {
                    Dimension = int.Parse(value);
                }
                else
                {
                    Dimension = null;
                }
            }
        }

        public CreateDimensionNodeViewModelBase(GeneratorViewModel calculator) : base(calculator)
        {
        }

        protected override void ConfigureNodeViewModel(TNodeViewModel model)
        {
            base.ConfigureNodeViewModel(model);
            model.SetDimension(Dimension.Value);
            model.Title = model.Title + " " + Dimension.Value + "D";
        }

        protected override bool CanCreate()
        {
            return Dimension != null && ValidationHelper.IsDimensionAllowed(Dimension.Value, this) && base.CanCreate();
        }
    }
}