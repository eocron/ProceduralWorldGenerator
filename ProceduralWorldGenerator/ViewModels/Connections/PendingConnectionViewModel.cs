using System.Windows;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels.Connections
{
    public class PendingNodeConnectionViewModel : ObservableObject
    {
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        public NodeConnectorViewModel Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        public NodeConnectorViewModel? Target
        {
            get => _target;
            set => SetProperty(ref _target, value);
        }

        public Point TargetLocation
        {
            get => _targetLocation;
            set => SetProperty(ref _targetLocation, value);
        }

        private bool _isVisible;
        private NodeConnectorViewModel _source = default!;

        private NodeConnectorViewModel? _target;

        private Point _targetLocation;
    }
}