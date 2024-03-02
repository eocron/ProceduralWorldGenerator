using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Spline
{
    public class CreateSplineNodeViewModel : CreateNodeViewModelBase<SplineNodeViewModel>
    {
        public CreateSplineNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
        }
    }
}