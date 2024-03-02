using System.Windows;
using Nodify.Shared;

namespace ProceduralWorldGenerator.ViewModels
{
    public class PendingCreateNodeViewModel : ObservableObject
    {
        private Point _location;
        private GeneratorPreviewNodeViewModel _preview;

        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public GeneratorPreviewNodeViewModel Preview
        {
            get => _preview;
            set => SetProperty(ref _preview, value);
        }
    }
}