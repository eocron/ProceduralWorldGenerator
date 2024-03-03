using System.Collections.ObjectModel;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Spline
{
    public class SplineEditorViewModel : ObservableObject
    {
        public SplineEditorClamp LeftClamp { get; set; }
        public SplineEditorClamp RightClamp { get; set; }
        public ObservableCollection<EditablePointViewModel> DataPoints { get; set; } = new ObservableCollection<EditablePointViewModel>();
    }
}