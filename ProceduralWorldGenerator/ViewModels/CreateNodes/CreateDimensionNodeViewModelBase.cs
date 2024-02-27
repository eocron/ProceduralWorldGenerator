using ProceduralWorldGenerator.Validation;
using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public abstract class CreateDimensionNodeViewModelBase<TNodeViewModel> : CreateNodeViewModelBase<TNodeViewModel>
        where TNodeViewModel : NodeViewModelBase, IDimensionModel
    {
        public CreateDimensionNodeViewModelBase(GeneratorViewModel calculator) : base(calculator)
        {
        }

        protected override void ConfigureNodeViewModel(TNodeViewModel model)
        {
            base.ConfigureNodeViewModel(model);
            model.Title = model.Title + " " + NodeViewModel.Dimension + "D";
        }

        protected override bool CanCreate()
        {
            return ValidationHelper.IsDimensionAllowed(NodeViewModel.Dimension, NodeViewModel) && base.CanCreate();
        }
    }
}