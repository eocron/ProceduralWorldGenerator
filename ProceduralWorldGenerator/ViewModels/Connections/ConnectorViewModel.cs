﻿using System.Collections.Generic;
using System.Windows;
using Nodify.Shared;
using ProceduralWorldGenerator.ViewModels.Nodes.Control;

namespace ProceduralWorldGenerator.ViewModels.Connections
{
    public class ConnectorViewModel : ObservableObject
    {
        private string? _variableTitle;
        public string? VariableTitle
        {
            get => _variableTitle;
            set
            {
                SetProperty(ref _variableTitle, value);
                OnPropertyChanged(nameof(Title));
            }
        }

        private string? _title;
        public string? Title
        {
            get => _variableTitle ?? _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        private bool _isInput;
        public bool IsInput
        {
            get => _isInput;
            set => SetProperty(ref _isInput, value);
        }

        private Point _anchor;
        public Point Anchor
        {
            get => _anchor;
            set => SetProperty(ref _anchor, value);
        }

        private OperationViewModel _operation = default!;
        public OperationViewModel Operation
        {
            get => _operation;
            set => SetProperty(ref _operation, value);
        }

        public List<ConnectorViewModel> ValueObservers { get; } = new List<ConnectorViewModel>();

        public void SetTitleFrom(ConnectorViewModel output)
        {
            VariableTitle = output.Title;
        }

        public void RestoreTitle()
        {
            VariableTitle = null;
        }
    }
}
