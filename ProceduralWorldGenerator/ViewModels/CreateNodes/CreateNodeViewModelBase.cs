using System.Linq;
using System.Windows;
using Nodify.Shared;
using ProceduralWorldGenerator.Helpers;
using ProceduralWorldGenerator.ViewModels.Nodes;
using ProceduralWorldGenerator.ViewModels.Nodes.Grouping;

namespace ProceduralWorldGenerator.ViewModels.CreateNodes
{
    public abstract class CreateNodeViewModelBase : ObservableObject
    {
        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        private Point _location;
        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }
        
                
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        
        public NodifyObservableCollection<string> ValidationErrors = new();

        public INodifyCommand CreateOperation { get; }
        public INodifyCommand CancelOperation { get; }

        public virtual void OpenAt(Point targetLocation)
        {
            Close();
            Location = targetLocation;
            IsVisible = true;
        }

        public void Close()
        {
            IsVisible = false;
        }

        protected readonly GeneratorViewModel Calculator;
        private string _description;

        protected CreateNodeViewModelBase(GeneratorViewModel calculator)
        {
            Calculator = calculator;
            CreateOperation = new RequeryCommand(OnCreateOperation);
            CancelOperation = new RequeryCommand(Close);
        }
        protected abstract void OnCreateOperation();

        public abstract void SetModel(NodeViewModelBase model);
    }
    
    public abstract class CreateNodeViewModelBase<TNodeViewModel> : CreateNodeViewModelBase
        where TNodeViewModel : NodeViewModelBase
    {
        private TNodeViewModel _nodeViewModel;
        public TNodeViewModel NodeViewModel
        {
            get => _nodeViewModel;
            set => SetProperty(ref _nodeViewModel, ObjectHelper.DeepCopy(value));
        }
        
        protected CreateNodeViewModelBase(GeneratorViewModel calculator) : base(calculator)
        {
        }

        protected override void OnCreateOperation()
        {
            var op = NodeCollectionViewModel.CreateNodeViewModel(NodeViewModel, ConfigureNodeViewModel);
            op.Location = Location;
            Calculator.Operations.Add(op);
            TryHandlePendingConnection(op);
            Close();
        }

        protected virtual void ConfigureNodeViewModel(TNodeViewModel model)
        {

        }

        public override void SetModel(NodeViewModelBase model)
        {
            NodeViewModel = (TNodeViewModel)model;
        }

        private void TryHandlePendingConnection(OperationViewModel op)
        {
            var pending = Calculator.PendingConnection;
            if (pending != null && pending.IsVisible)
            {
                var connector = pending.Source.IsInput ? op.Output.FirstOrDefault() : op.Input.FirstOrDefault();
                if (connector != null && Calculator.CanCreateConnection(pending.Source, connector))
                {
                    Calculator.CreateConnection(pending.Source, connector);
                }
            }
        }
    }
}