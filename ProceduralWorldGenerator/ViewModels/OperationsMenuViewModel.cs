using System;
using System.Collections.Generic;
using System.Windows;
using Nodify.Shared;
using ProceduralWorldGenerator.ViewModels.Nodes;

namespace ProceduralWorldGenerator.ViewModels
{
    public class OperationsMenuViewModel : ObservableObject
    {
        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                SetProperty(ref _isVisible, value);
                if (!value)
                {
                    Closed?.Invoke();
                }
            }
        }

        private Point _location;
        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public event Action? Closed;

        public void OpenAt(Point targetLocation)
        {
            Close();
            Location = targetLocation;
            IsVisible = true;
        }

        public void Close()
        {
            IsVisible = false;
        }

        public NodifyObservableCollection<NodeViewModelBase> AvailableOperations { get; }
        public INodifyCommand CreateOperationCommand { get; }
        private readonly GeneratorViewModel _calculator;

        public OperationsMenuViewModel(GeneratorViewModel calculator)
        {
            _calculator = calculator;
            var operations = new List<NodeViewModelBase>();
            
            operations.AddRange(NodePreviewProvider.GetPreviews());

            AvailableOperations = new NodifyObservableCollection<NodeViewModelBase>(operations);
            CreateOperationCommand = new DelegateCommand<NodeViewModelBase>(CreateOperation);
        }

        private void CreateOperation(NodeViewModelBase operationInfo)
        {
            _calculator.CreateNode(Location, operationInfo);
            Close();
        }
    }
}
