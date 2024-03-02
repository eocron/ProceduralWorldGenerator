using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public class CreateSplineNodeViewModel : CreateNodeViewModelBase<SplineNodeViewModel>
    {
        public CreateSplineNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
            Description = "Spline";
        }
    }
}