using System.Windows;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels.Connections
{
    public class PendingNodeConnectionViewModel : ObservableObject
    {
        private NodeConnectorViewModel _source = default!;
        public NodeConnectorViewModel Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        private NodeConnectorViewModel? _target;
        public NodeConnectorViewModel? Target
        {
            get => _target;
            set => SetProperty(ref _target, value);
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        private Point _targetLocation;

        public Point TargetLocation
        {
            get => _targetLocation;
            set => SetProperty(ref _targetLocation, value);
        }
    }
}
