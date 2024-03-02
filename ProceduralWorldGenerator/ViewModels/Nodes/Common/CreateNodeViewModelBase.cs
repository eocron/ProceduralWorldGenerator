using System.Linq;
using System.Windows;
using Nodify.Shared;
using ProceduralWorldGenerator.Helpers;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public abstract class CreateMenuViewModelBase : ObservableObject
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

        protected CreateMenuViewModelBase()
        {
            CreateOperation = new RequeryCommand(OnCreateOperation);
            CancelOperation = new RequeryCommand(Close);
        }
        protected abstract void OnCreateOperation();

        /// <summary>
        /// Will copy model and assign copy to NodeViewModel
        /// </summary>
        /// <param name="model"></param>
        public abstract void SetModelTemplate(NodeViewModelBase model);
    }
    
    public abstract class CreateMenuViewModelBase<TNodeViewModel> : CreateMenuViewModelBase
        where TNodeViewModel : NodeViewModelBase
    {
        protected readonly GeneratorViewModel Calculator;
        private TNodeViewModel _nodeViewModel;
        public TNodeViewModel NodeViewModel
        {
            get => _nodeViewModel;
            private set => SetProperty(ref _nodeViewModel, ObjectHelper.DeepCopy(value));
        }
        
        protected CreateMenuViewModelBase(GeneratorViewModel calculator) : base()
        {
            Calculator = calculator;
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

        public override void SetModelTemplate(NodeViewModelBase model)
        {
            NodeViewModel = (TNodeViewModel)model;
        }

        private void TryHandlePendingConnection(GeneratorNodeViewModel op)
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