using System.Windows;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels
{
    public class PendingCreateNodeViewModel : ObservableObject
    {
        public GeneratorPreviewNodeViewModel Preview
        {
            get => _preview;
            set => SetProperty(ref _preview, value);
        }

        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private GeneratorPreviewNodeViewModel _preview;
        private Point _location;
    }
}