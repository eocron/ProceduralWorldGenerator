using System;
using System.Windows;
using ProceduralWorldGenerator.Common;

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

        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        private Point _location;
        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }
        
        private object _newModel;
        public object NewModel
        {
            get => _newModel;
            set => SetProperty(ref _newModel, value);
        }
        
        private object _prevModel;
        public object PrevModel
        {
            get => _prevModel;
            set => SetProperty(ref _prevModel, value);
        }

        public NodeSyntaxViewModel Syntax
        {
            get => _syntax;
            set => SetProperty(ref _syntax, value);
        }

        public EventHandler OnCreateInvoked;
        public EventHandler OnEditInvoked;
        
        private NodeSyntaxViewModel _syntax;
        private bool _isEdit;

        public INodifyCommand CreateOperation { get; }
        public INodifyCommand CancelOperation { get; }

        public virtual void ShowCreateDialog()
        {
            Close();
            IsEdit = false;
            IsVisible = true;
        }
        
        public virtual void ShowEditDialog()
        {
            Close();
            IsEdit = true;
            IsVisible = true;
        }

        public void Close()
        {
            IsVisible = false;
        }

        protected CreateMenuViewModelBase()
        {
            CreateOperation = new RequeryCommand(()=>
            {
                if (IsEdit)
                {
                    OnEditInvoked?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    OnCreateInvoked?.Invoke(this, EventArgs.Empty);
                }
                Close();
            });
            CancelOperation = new RequeryCommand(Close);
        }
    }
}