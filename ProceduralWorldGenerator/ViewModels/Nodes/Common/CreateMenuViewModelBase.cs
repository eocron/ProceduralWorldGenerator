using System;
using System.Windows;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public abstract class CreateMenuViewModelBase : ObservableObject
    {
        protected CreateMenuViewModelBase()
        {
            CreateOperation = new RequeryCommand(() =>
            {
                if (IsEdit)
                    OnEditInvoked?.Invoke(this, EventArgs.Empty);
                else
                    OnCreateInvoked?.Invoke(this, EventArgs.Empty);
                Close();
            });
            CancelOperation = new RequeryCommand(Close);
        }

        public void Close()
        {
            IsVisible = false;
        }

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

        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        public INodifyCommand CancelOperation { get; }

        public INodifyCommand CreateOperation { get; }

        public NodeSyntaxViewModel Syntax
        {
            get => _syntax;
            set => SetProperty(ref _syntax, value);
        }

        public object NewModel
        {
            get => _newModel;
            set => SetProperty(ref _newModel, value);
        }

        public object PrevModel
        {
            get => _prevModel;
            set => SetProperty(ref _prevModel, value);
        }

        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public EventHandler OnCreateInvoked;
        public EventHandler OnEditInvoked;
        private bool _isEdit;
        private bool _isVisible;

        private NodeSyntaxViewModel _syntax;

        private object _newModel;

        private object _prevModel;

        private Point _location;
    }
}