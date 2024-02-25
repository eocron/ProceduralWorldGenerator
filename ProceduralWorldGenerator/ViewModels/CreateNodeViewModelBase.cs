using System.Linq;
using System.Windows;
using Nodify.Shared;
using ProceduralWorldGenerator.ViewModels.Nodes.Control;

namespace ProceduralWorldGenerator.ViewModels
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

        protected CreateNodeViewModelBase(GeneratorViewModel calculator)
        {
            Calculator = calculator;
            CreateOperation = new RequeryCommand(OnCreateOperation, CanCreate);
            CancelOperation = new RequeryCommand(Close);
        }

        protected virtual bool CanCreate()
        {
            return true;
        }
        
        protected virtual void OnCreateOperation()
        {
            var op = Create();
            op.Location = Location;
            Calculator.Operations.Add(op);
            TryHandlePendingConnection(op);
            Close();
        }

        protected abstract OperationViewModel Create();
        
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