﻿using System;
using System.Windows;
using Nodify.Shared;

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
        
        private object _model;
        public object Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        public EventHandler OnCreateInvoked;
        
        public INodifyCommand CreateOperation { get; }
        public INodifyCommand CancelOperation { get; }

        public virtual void Show()
        {
            Close();
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
                OnCreateOperation();
                OnCreateInvoked?.Invoke(this, EventArgs.Empty);
                Close();
            });
            CancelOperation = new RequeryCommand(Close);
        }

        protected virtual void OnCreateOperation()
        {
        }
    }
}