using System.ComponentModel;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Spline
{
    public class SplineEditorViewModel : ObservableObject
    {
        public SplineEditorClamp LeftClamp { get; set; }
        public SplineEditorClamp RightClamp { get; set; }
        public BindingList<EditablePointViewModel> DataPoints { get; set; } = new BindingList<EditablePointViewModel>();
    }
}