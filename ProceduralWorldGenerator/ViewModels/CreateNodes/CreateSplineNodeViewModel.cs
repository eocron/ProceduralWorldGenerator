using ProceduralWorldGenerator.ViewModels.Nodes;
using ProceduralWorldGenerator.ViewModels.Splines;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public class CreateSplineNodeViewModel : CreateNodeViewModelBase<SplineNodeViewModel>
    {
        public SplineEditorViewModel Spline { get; set; }
        public CreateSplineNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
            Description = "Spline";
            Spline = new SplineEditorViewModel();
        }
    }
}