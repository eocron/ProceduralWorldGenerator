using System.Windows;
using Nodify.Shared;
using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels
{
    public class PendingCreateNodeViewModel : ObservableObject
    {
        private Point _location;
        private NodeViewModelBase _nodeViewModel;

        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public NodeViewModelBase NodeViewModel
        {
            get => _nodeViewModel;
            set => SetProperty(ref _nodeViewModel, value);
        }
    }
}