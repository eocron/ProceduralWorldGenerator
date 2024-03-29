﻿using System.Linq;
using System.Windows;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.Helpers;
using ProceduralWorldGenerator.ViewModels.Connections;
using ProceduralWorldGenerator.ViewModels.Nodes;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels
{
    public class GeneratorViewModel : ObservableObject
    {
        public NodeSyntaxViewModel Syntax { get; }
        public NodifyObservableCollection<NodeConnectionViewModel> Connections { get; } = new();
        public PendingNodeConnectionViewModel PendingConnection { get; set; } = new();
        public OperationsMenuViewModel OperationsMenu { get; set; }

        public PendingCreateNodeViewModel PendingCreateNodeMenu { get; set; }

        public CreateMenuViewModelBase CreateNodeMenu
        {
            get => _createNodeMenu;
            set => SetProperty(ref _createNodeMenu, value);
        }

        public INodifyCommand StartConnectionCommand { get; }
        public INodifyCommand CreateConnectionCommand { get; }
        public INodifyCommand DisconnectConnectorCommand { get; }
        public INodifyCommand DeleteSelectionCommand { get; }
        public INodifyCommand GroupSelectionCommand { get; }
        public INodifyCommand CreateNodeCommand { get; set; }
        
        public INodifyCommand EditNodeCommand { get; set; }
        
        private NodifyObservableCollection<GeneratorNodeViewModel> _operations = new();
        public NodifyObservableCollection<GeneratorNodeViewModel> Operations
        {
            get => _operations;
            set => SetProperty(ref _operations, value);
        }

        private NodifyObservableCollection<GeneratorNodeViewModel> _selectedOperations = new();
        private CreateMenuViewModelBase _createNodeMenu;

        public NodifyObservableCollection<GeneratorNodeViewModel> SelectedOperations
        {
            get => _selectedOperations;
            set => SetProperty(ref _selectedOperations, value);
        }

        public NodeCollectionViewModel NodeCollectionModel { get; set; }

        public GeneratorViewModel()
        {
            CreateConnectionCommand = new DelegateCommand<NodeConnectorViewModel>(
                _ => CreateConnection(PendingConnection.Source, PendingConnection.Target),
                _ => CanCreateConnection(PendingConnection.Source, PendingConnection.Target));
            StartConnectionCommand = new DelegateCommand<NodeConnectorViewModel>(_ => PendingConnection.IsVisible = true, (c) => !(c.IsConnected && c.IsInput));
            DisconnectConnectorCommand = new DelegateCommand<NodeConnectorViewModel>(DisconnectConnector);
            DeleteSelectionCommand = new DelegateCommand(DeleteSelection);
            GroupSelectionCommand = new DelegateCommand(GroupSelectedOperations, () => SelectedOperations.Count > 0);
            CreateNodeCommand = new DelegateCommand(() =>
                OpenNodeCreationDialog(PendingCreateNodeMenu.Location, PendingCreateNodeMenu.Preview));
            EditNodeCommand = new DelegateCommand(() =>
                OpenNodeEditDialog(SelectedOperations.Single()), ()=> SelectedOperations.Count == 1 && SelectedOperations.Single().NodeModel.SupportEdit);

            Connections.WhenAdded(c =>
            {
                c.Input.IsConnected = true;
                c.Output.IsConnected = true;
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
            });

            Operations.WhenAdded(x =>
            {
                x.Input.WhenRemoved(RemoveConnection);

                void RemoveConnection(NodeConnectorViewModel i)
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

            Syntax = new NodeSyntaxViewModel();
            NodeCollectionModel = new NodeCollectionViewModel();
            OperationsMenu = new OperationsMenuViewModel(this, NodeCollectionModel);
            PendingCreateNodeMenu = new PendingCreateNodeViewModel();
            foreach (var createMenu in NodeCollectionModel.GetCreateMenus())
            {
                createMenu.OnCreateInvoked += (menu, _) => OnCreateOperation((CreateMenuViewModelBase)menu);
                createMenu.OnEditInvoked += (menu, _) => OnEditOperation((CreateMenuViewModelBase)menu);
                createMenu.Syntax = Syntax;
            }
            CreateNodeMenu = NodeCollectionModel.GetCreateMenus().First();
        }

        private void OpenNodeEditDialog(GeneratorNodeViewModel model)
        {
            var menu = NodeCollectionModel.GetCreateMenuViewModel(model.NodeModel.GetType());
            menu.PrevModel = model.NodeModel;
            menu.NewModel = ObjectHelper.DeepClone<NodeViewModelBase>(model.NodeModel);
            menu.Location = model.Location;
            CreateNodeMenu = menu;
            CreateNodeMenu.ShowEditDialog();
        }

        private void OpenNodeCreationDialog(Point location, GeneratorPreviewNodeViewModel preview)
        {
            var menu = NodeCollectionModel.GetCreateMenuViewModel(preview.NodeType);
            menu.PrevModel = null;
            menu.NewModel = NodeCollectionModel.CreateTemplateNodeViewModel(preview.NodeType);
            menu.Location = location;
            CreateNodeMenu = menu;
            CreateNodeMenu.ShowCreateDialog();
        }

        private void OnEditOperation(CreateMenuViewModelBase menu)
        {
            var generatorModel = Operations.Where(x => x.NodeModel == menu.PrevModel).Single();
            generatorModel.NodeModel = (NodeViewModelBase)menu.NewModel;
            menu.Close();
        }
        
        private void OnCreateOperation(CreateMenuViewModelBase menu)
        {
            var op = NodeCollectionModel.CreateGeneratorNodeViewModel((NodeViewModelBase)menu.NewModel);
            op.Location = menu.Location;
            this.Operations.Add(op);
            Syntax.AddVariableName(op.NodeModel.VariableName);
            TryHandlePendingConnection(op);
            menu.Close();
        }

        private void TryHandlePendingConnection(GeneratorNodeViewModel op)
        {
            var pending = this.PendingConnection;
            if (pending != null && pending.IsVisible)
            {
                var connector = pending.Source.IsInput ? op.Output.FirstOrDefault() : op.Input.FirstOrDefault();
                if (connector != null && this.CanCreateConnection(pending.Source, connector))
                {
                    this.CreateConnection(pending.Source, connector);
                }
            }
        }


        private void DisconnectConnector(NodeConnectorViewModel connector)
        {
            var connections = Connections.Where(c => c.Input == connector || c.Output == connector).ToList();
            connections.ForEach(c => Connections.Remove(c));
        }

        private bool CanCreateConnection(NodeConnectorViewModel source, NodeConnectorViewModel? target)
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

        private void CreateConnection(NodeConnectorViewModel source, NodeConnectorViewModel? target)
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

            Connections.Add(new NodeConnectionViewModel
            {
                Input = input,
                Output = output,
            });
        }

        private void OnOperationsMenuClosed()
        {
            PendingConnection.IsVisible = false;
            OperationsMenu.Closed -= OnOperationsMenuClosed;
        }

        private void DeleteSelection()
        {
            var selected = SelectedOperations.ToList();
            selected.ForEach(o =>
            {
                Operations.Remove(o);
                Syntax.DeleteVariableName(o.NodeModel.VariableName);
            });
        }

        private void GroupSelectedOperations()
        {
            var selected = SelectedOperations.ToList();
            var bounding = selected.GetBoundingBox(50);

            Operations.Add(new OperationGroupViewModel
            {
                Location = bounding.Location,
                GroupSize = new Size(bounding.Width, bounding.Height)
            });
        }
    }
}

