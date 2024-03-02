using System.Windows;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public class OperationGroupViewModel : GeneratorNodeViewModel
    {
        private Size _size;
        public Size GroupSize
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }
    }
}