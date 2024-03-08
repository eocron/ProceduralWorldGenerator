﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.Helpers;
using ProceduralWorldGenerator.ViewModels.Connections;
using ProceduralWorldGenerator.ViewModels.Nodes;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GeneratorViewModel : ObservableObject
    {
        public GeneratorViewModel()
        {
            CreateConnectionCommand = new DelegateCommand<NodeConnectorViewModel>(
                _ => CreateConnection(PendingConnection.Source, PendingConnection.Target),
                _ => CanCreateConnection(PendingConnection.Source, PendingConnection.Target));
            StartConnectionCommand =
                new DelegateCommand<NodeConnectorViewModel>(_ => PendingConnection.IsVisible = true,
                    c => !(c.IsConnected && c.IsInput));
            DisconnectConnectorCommand = new DelegateCommand<NodeConnectorViewModel>(DisconnectConnector);
            DeleteSelectionCommand = new DelegateCommand(DeleteSelection);
            GroupSelectionCommand = new DelegateCommand(GroupSelectedOperations, () => SelectedOperations.Count > 0);
            CreateNodeCommand = new DelegateCommand(() =>
                OpenNodeCreationDialog(PendingCreateNodeMenu.Location, PendingCreateNodeMenu.Preview));
            EditNodeCommand = new DelegateCommand(() =>
                    OpenNodeEditDialog(SelectedOperations.Single()),
                () => SelectedOperations.Count == 1 && SelectedOperations.Single().NodeModel.SupportEdit);

            Connections
                .WhenAdded(c =>
                {
                    c.Input = FindNodeConnector(c.Input);
                    c.Output = FindNodeConnector(c.Output);
                    c.Input.IsConnected = true;
                    c.Output.IsConnected = true;
                })
                .WhenRemoved(c =>
                {
                    var ic = FindRelatedConnections(c.Input).Count();
                    var oc = FindRelatedConnections(c.Output).Count();

                    if (ic == 0)
                    {
                        c.Input.IsConnected = false;
                    }

                    if (oc == 0)
                    {
                        c.Output.IsConnected = false;
                    }
                });

            Operations
                .WhenAdded(x =>
                {
                    x.Input.WhenRemoved(RemoveConnection);

                    void RemoveConnection(NodeConnectorViewModel i)
                    {
                        var c = FindRelatedConnections(i).ToList();
                        c.ForEach(con => Connections.Remove(con));
                    }
                })
                .WhenRemoved(x =>
                {
                    foreach (var input in x.Input) DisconnectConnector(input);

                    foreach (var output in x.Output) DisconnectConnector(output);
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

        private bool CanCreateConnection(NodeConnectorViewModel source, NodeConnectorViewModel? target)
        {
            if (target == null) return true; //prompt for new operation

            if (source == target || source.NodeId == target.NodeId) return false; //same operation

            if (source.IsInput == target.IsInput) return false; //input to input connection

            var src = FindParameterById(source.NodeParameterId);
            var tgt = FindParameterById(target.NodeParameterId);
            if (!ParameterTypeEqualityComparer.Instance.Equals(src, tgt)) return false;

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
                Output = output
            });
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


        private void DisconnectConnector(NodeConnectorViewModel connector)
        {
            var connections = FindRelatedConnections(connector).ToList();
            connections.ForEach(c => Connections.Remove(c));
        }

        private IEnumerable<NodeConnectionViewModel> FindRelatedConnections(NodeConnectorViewModel connectorViewModel)
        {
            return Connections.Where(c =>
                NodeConnectorViewModelEqualityComparer.Instance.Equals(c.Input, connectorViewModel) ||
                NodeConnectorViewModelEqualityComparer.Instance.Equals(c.Output, connectorViewModel));
        }
        
        private NodeConnectorViewModel FindNodeConnector(NodeConnectorViewModel connectorViewModel)
        {
            return Operations.SelectMany(x => x.Input)
                .Concat(Operations.SelectMany(x => x.Output))
                .Single(c => NodeConnectorViewModelEqualityComparer.Instance.Equals(c, connectorViewModel));
        }

        private NodeViewModelBase FindNodeById(string nodeId)
        {
            return Operations.Single(x => x.NodeModel.Id == nodeId).NodeModel;
        }

        private ParameterViewModelBase FindParameterById(string parameterId)
        {
            return Operations.SelectMany(x => x.GetParameterModels()).Single(x => x.Id == parameterId);
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

        private void OnCreateOperation(CreateMenuViewModelBase menu)
        {
            var op = NodeCollectionModel.CreateGeneratorNodeViewModel((NodeViewModelBase)menu.NewModel);
            op.Location = menu.Location;
            Operations.Add(op);
            Syntax.AddVariableName(op.NodeModel.VariableName);
            TryHandlePendingConnection(op);
            menu.Close();
        }

        private void OnEditOperation(CreateMenuViewModelBase menu)
        {
            var generatorModel = Operations.Where(x => x.NodeModel == menu.PrevModel).Single();
            generatorModel.NodeModel = (NodeViewModelBase)menu.NewModel;
            menu.Close();
        }

        private void OnOperationsMenuClosed()
        {
            PendingConnection.IsVisible = false;
            OperationsMenu.Closed -= OnOperationsMenuClosed;
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

        private void OpenNodeEditDialog(GeneratorNodeViewModel model)
        {
            var menu = NodeCollectionModel.GetCreateMenuViewModel(model.NodeModel.GetType());
            menu.PrevModel = model.NodeModel;
            menu.NewModel = ObjectHelper.DeepClone(model.NodeModel);
            menu.Location = model.Location;
            CreateNodeMenu = menu;
            CreateNodeMenu.ShowEditDialog();
        }

        private void TryHandlePendingConnection(GeneratorNodeViewModel op)
        {
            var pending = PendingConnection;
            if (pending != null && pending.IsVisible)
            {
                var connector = pending.Source.IsInput ? op.Output.FirstOrDefault() : op.Input.FirstOrDefault();
                if (connector != null && CanCreateConnection(pending.Source, connector))
                    CreateConnection(pending.Source, connector);
            }
        }

        public CreateMenuViewModelBase CreateNodeMenu
        {
            get => _createNodeMenu;
            set => SetProperty(ref _createNodeMenu, value);
        }

        public INodifyCommand CreateConnectionCommand { get; }
        public INodifyCommand CreateNodeCommand { get; set; }
        public INodifyCommand DeleteSelectionCommand { get; }
        public INodifyCommand DisconnectConnectorCommand { get; }
        public INodifyCommand EditNodeCommand { get; set; }
        public INodifyCommand GroupSelectionCommand { get; }

        public INodifyCommand StartConnectionCommand { get; }

        public NodeCollectionViewModel NodeCollectionModel { get; set; }

        [JsonProperty]
        public NodeSyntaxViewModel Syntax
        {
            get => _syntax;
            set => SetProperty(ref _syntax, value);
        }

        [JsonProperty]
        public NodifyObservableCollection<GeneratorNodeViewModel> Operations
        {
            get => _operations;
            set => SetProperty(ref _operations, value);
        }

        public NodifyObservableCollection<GeneratorNodeViewModel> SelectedOperations
        {
            get => _selectedOperations;
            set => SetProperty(ref _selectedOperations, value);
        }

        [JsonProperty]
        public NodifyObservableCollection<NodeConnectionViewModel> Connections
        {
            get => _connections;
            set => SetProperty(ref _connections, value);
        }

        public OperationsMenuViewModel OperationsMenu { get; set; }

        public PendingCreateNodeViewModel PendingCreateNodeMenu { get; set; }

        public PendingNodeConnectionViewModel PendingConnection { get; set; } = new();
        private CreateMenuViewModelBase _createNodeMenu;
        private NodeSyntaxViewModel _syntax;
        private NodifyObservableCollection<GeneratorNodeViewModel> _operations = new();
        private NodifyObservableCollection<GeneratorNodeViewModel> _selectedOperations = new();
        private NodifyObservableCollection<NodeConnectionViewModel> _connections = new();
    }
}