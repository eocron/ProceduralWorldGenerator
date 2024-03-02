using System.Collections.ObjectModel;
using System.Windows;
using Nodify.Shared;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Spline
{
    public class SplineEditorViewModel : ObservableObject
    {
        public SplineEditorClamp LeftClamp { get; set; }
        public SplineEditorClamp RightClamp { get; set; }
        public ObservableCollection<Point> DataPoints { get; set; } = new ObservableCollection<Point>();
    }
}