using System.Linq;
using System.Windows;
using Nodify.Shared;
using ProceduralWorldGenerator.Validation;
using ProceduralWorldGenerator.ViewModels.Connections;
using ProceduralWorldGenerator.ViewModels.Nodes;
using ProceduralWorldGenerator.ViewModels.Nodes.Grouping;

namespace ProceduralWorldGenerator.ViewModels
{
    public class GeneratorViewModel : ObservableObject
    {
        public GeneratorViewModel()
        {
            CreateConnectionCommand = new DelegateCommand<ConnectorViewModel>(
                _ => CreateConnection(PendingConnection.Source, PendingConnection.Target),
                _ => CanCreateConnection(PendingConnection.Source, PendingConnection.Target));
            StartConnectionCommand = new DelegateCommand<ConnectorViewModel>(_ => PendingConnection.IsVisible = true, (c) => !(c.IsConnected && c.IsInput));
            DisconnectConnectorCommand = new DelegateCommand<ConnectorViewModel>(DisconnectConnector);
            DeleteSelectionCommand = new DelegateCommand(DeleteSelection);
            GroupSelectionCommand = new DelegateCommand(GroupSelectedOperations, () => SelectedOperations.Count > 0);

            Connections.WhenAdded(c =>
            {
                c.Input.IsConnected = true;
                c.Output.IsConnected = true;
                c.Input.SetTitleFrom(c.Output);
            })
            .WhenRemoved(c =>
            {
                var ic = Connections.Count(con => con.Input == c.Input || con.Output == c.Input);
                var oc = Connections.Count(con => con.Input == c.Output || con.Output == c.Output);

                if (ic == 0)
                {
                    c.Input.IsConnected = false;
                }

                if (oc == 0)
                {
                    c.Output.IsConnected = false;
                }

                c.Input.RestoreTitle();
            });

            Operations.WhenAdded(x =>
            {
                x.Input.WhenRemoved(RemoveConnection);

                void RemoveConnection(ConnectorViewModel i)
                {
                    var c = Connections.Where(con => con.Input == i || con.Output == i).ToArray();
                    c.ForEach(con => Connections.Remove(con));
                }
            })
            .WhenRemoved(x =>
            {
                foreach (var input in x.Input)
                {
                    DisconnectConnector(input);
                }

                foreach (var output in x.Output)
                {
                    DisconnectConnector(output);
                }
            });

            OperationsMenu = new OperationsMenuViewModel(this);
            CreateDimensionNodeMenu = new CreateDimensionNodeViewModel(this);
            CreateDefaultNodeMenu = new CreateDefaultNodeViewModel(this);
        }

        private NodifyObservableCollection<OperationViewModel> _operations = new NodifyObservableCollection<OperationViewModel>();
        public NodifyObservableCollection<OperationViewModel> Operations
        {
            get => _operations;
            set => SetProperty(ref _operations, value);
        }

        private NodifyObservableCollection<OperationViewModel> _selectedOperations = new NodifyObservableCollection<OperationViewModel>();
        public NodifyObservableCollection<OperationViewModel> SelectedOperations
        {
            get => _selectedOperations;
            set => SetProperty(ref _selectedOperations, value);
        }

        public NodifyObservableCollection<ConnectionViewModel> Connections { get; } = new NodifyObservableCollection<ConnectionViewModel>();
        public PendingConnectionViewModel PendingConnection { get; set; } = new PendingConnectionViewModel();
        public OperationsMenuViewModel OperationsMenu { get; set; }
        public CreateDimensionNodeViewModel CreateDimensionNodeMenu { get; set; }
        public CreateDefaultNodeViewModel CreateDefaultNodeMenu { get; set; }
        public INodifyCommand StartConnectionCommand { get; }
        public INodifyCommand CreateConnectionCommand { get; }
        public INodifyCommand DisconnectConnectorCommand { get; }
        public INodifyCommand DeleteSelectionCommand { get; }
        public INodifyCommand GroupSelectionCommand { get; }


        private void DisconnectConnector(ConnectorViewModel connector)
        {
            var connections = Connections.Where(c => c.Input == connector || c.Output == connector).ToList();
            connections.ForEach(c => Connections.Remove(c));
        }

        internal bool CanCreateConnection(ConnectorViewModel source, ConnectorViewModel? target)
        {
            if (target == null)
            {
                return true;//prompt for new operation
            }

            if (source == target || source.Operation == target.Operation)
            {
                return false;//same operation
            }

            if (source.IsInput == target.IsInput)
            {
                return false;//input to input connection
            }

            if (!source.CanConnect(target))
            {
                return false;
            }
            
            return true;
        }

        internal void CreateConnection(ConnectorViewModel source, ConnectorViewModel? target)
        {
            if (target == null)
            {
                PendingConnection.IsVisible = true;
                OperationsMenu.OpenAt(PendingConnection.TargetLocation);
                OperationsMenu.Closed += OnOperationsMenuClosed;
                return;
            }

            var input = source.IsInput ? source : target;
            var output = target.IsInput ? source : target;

            PendingConnection.IsVisible = false;

            DisconnectConnector(input);

            Connections.Add(new ConnectionViewModel
            {
                Input = input,
                Output = output,
            });
        }

        internal void CreateNode(Point location, NodeViewModelBase previewNode)
        {
            if (previewNode is IDimensionSetter dimensionSetter)
            {
                CreateDimensionNodeMenu.MinDimension = dimensionSetter.MinDimension;
                CreateDimensionNodeMenu.MaxDimension = dimensionSetter.MaxDimension;
                CreateDimensionNodeMenu.AllowedDimensions = dimensionSetter.AllowedDimensions;

                CreateDimensionNodeMenu.OperationViewModelProvider =
                    () => NodePreviewProvider.CreateNodeViewModel(previewNode);
                CreateDimensionNodeMenu.OpenAt(location);
            }
            else
            {
                CreateDefaultNodeMenu.OperationViewModelProvider =
                    () => NodePreviewProvider.CreateNodeViewModel(previewNode);
                CreateDefaultNodeMenu.OpenAt(location);
            }
        }

        private void OnOperationsMenuClosed()
        {
            PendingConnection.IsVisible = false;
            OperationsMenu.Closed -= OnOperationsMenuClosed;
        }

        private void DeleteSelection()
        {
            var selected = SelectedOperations.ToList();
            selected.ForEach(o => Operations.Remove(o));
        }

        private void GroupSelectedOperations()
        {
            var selected = SelectedOperations.ToList();
            var bounding = selected.GetBoundingBox(50);

            Operations.Add(new OperationGroupViewModel
            {
                Title = "Operations",
                Location = bounding.Location,
                GroupSize = new Size(bounding.Width, bounding.Height)
            });
        }
    }
}

