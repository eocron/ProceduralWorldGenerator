using System;
using System.Collections.Generic;
using System.Windows;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels
{
    public class OperationsMenuViewModel : ObservableObject
    {
        public OperationsMenuViewModel(GeneratorViewModel calculator, NodeCollectionViewModel collectionViewModel)
        {
            _calculator = calculator;
            var operations = new List<GeneratorPreviewNodeViewModel>();

            operations.AddRange(collectionViewModel.GetNodePreviews());

            AvailableOperations = new NodifyObservableCollection<GeneratorPreviewNodeViewModel>(operations);
            CreateOperationCommand = new DelegateCommand<GeneratorPreviewNodeViewModel>(CreateOperation);
        }

        public void Close()
        {
            IsVisible = false;
        }

        public event Action? Closed;

        public void OpenAt(Point targetLocation)
        {
            Close();
            Location = targetLocation;
            IsVisible = true;
        }

        private void CreateOperation(GeneratorPreviewNodeViewModel operationInfo)
        {
            _calculator.PendingCreateNodeMenu.Preview = operationInfo;
            _calculator.PendingCreateNodeMenu.Location = Location;
            _calculator.CreateNodeCommand.Execute(null);
            Close();
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                SetProperty(ref _isVisible, value);
                if (!value) Closed?.Invoke();
            }
        }

        public INodifyCommand CreateOperationCommand { get; }

        public NodifyObservableCollection<GeneratorPreviewNodeViewModel> AvailableOperations { get; }

        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private readonly GeneratorViewModel _calculator;
        private bool _isVisible;

        private Point _location;
    }
}