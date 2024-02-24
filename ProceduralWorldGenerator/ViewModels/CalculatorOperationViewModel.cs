using System.Windows;
using Nodify.Shared;

namespace ProceduralWorldGenerator.ViewModels
{
    public class CalculatorOperationViewModel : OperationViewModel
    {
        public CalculatorViewModel InnerCalculator { get; } = new CalculatorViewModel();

        private OperationViewModel InnerOutput { get; } = new OperationViewModel
        {
            Title = "Output Parameters",
            Input = { new ConnectorViewModel() },
            Location = new Point(500, 300),
            IsReadOnly = true
        };

        private CalculatorInputOperationViewModel InnerInput { get; } = new CalculatorInputOperationViewModel
        {
            Title = "Input Parameters",
            Location = new Point(300, 300),
            IsReadOnly = true
        };

        public CalculatorOperationViewModel()
        {
            InnerCalculator.Operations.Add(InnerInput);
            InnerCalculator.Operations.Add(InnerOutput);

            Output.Add(new ConnectorViewModel());

            InnerOutput.Input[0].ValueObservers.Add(Output[0]);

            InnerInput.Output.ForEach(x => Input.Add(new ConnectorViewModel
            {
                Title = x.Title
            }));

            InnerInput.Output
                .WhenAdded(x => Input.Add(new ConnectorViewModel
                {
                    Title = x.Title
                }))
                .WhenRemoved(x => Input.RemoveOne(i => i.Title == x.Title));
        }

        protected override void OnInputValueChanged()
        {
            
        }
    }
}
