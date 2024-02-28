using Nodify.Shared;

namespace Debug
{
    public class MainWindowViewModel : ObservableObject
    {
        public SplineEditorViewModel Spline { get; set; }
        public MainWindowViewModel()
        {
            Spline = new SplineEditorViewModel();
        }
    }
}