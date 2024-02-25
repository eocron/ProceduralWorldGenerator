using System;
using System.Windows;
using ProceduralWorldGenerator.ViewModels.Nodes.Grouping;

namespace ProceduralWorldGenerator.ViewModels
{
    public class CreateDefaultNodeViewModel : CreateNodeViewModelBase
    {
        public Func<OperationViewModel> OperationViewModelProvider { get; set; }
        public CreateDefaultNodeViewModel(GeneratorViewModel calculator) : base(calculator)
        {
        }

        public override void OpenAt(Point targetLocation)
        {
            base.OpenAt(targetLocation);
            OnCreateOperation();
            Close();
        }

        protected override OperationViewModel Create()
        {
            return OperationViewModelProvider();
        }
    }
}