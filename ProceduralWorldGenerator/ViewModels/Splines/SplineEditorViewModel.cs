using System.Collections.ObjectModel;
using System.Windows;
using Nodify.Shared;
using ProceduralWorldGenerator.Views.Splines;

namespace ProceduralWorldGenerator.ViewModels.Splines
{
    public class SplineEditorViewModel : ObservableObject
    {
        public SplineEditorClamp LeftClamp { get; set; } = SplineEditorClamp.PingPong;
        public SplineEditorClamp RightClamp { get; set; } = SplineEditorClamp.PingPong;
        public ObservableCollection<Point> DataPoints { get; set; } = new ObservableCollection<Point>(){new Point(0,0), new Point(1,1)};
    }
}