using System;
using Nodify.Shared;

namespace ProceduralWorldGenerator.ViewModels
{
    public class EditorViewModel : ObservableObject
    {
        public event Action<EditorViewModel, GeneratorViewModel>? OnOpenInnerCalculator;

        public EditorViewModel? Parent { get; set; }

        public EditorViewModel()
        {
            Calculator = new GeneratorViewModel();
            OpenCalculatorCommand = new DelegateCommand<GeneratorViewModel>(calculator =>
            {
                OnOpenInnerCalculator?.Invoke(this, calculator);
            });
        }

        public INodifyCommand OpenCalculatorCommand { get; }

        public Guid Id { get; } = Guid.NewGuid();

        private GeneratorViewModel _calculator = default!;
        public GeneratorViewModel Calculator 
        {
            get => _calculator;
            set => SetProperty(ref _calculator, value);
        }

        private string? _name;
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}
