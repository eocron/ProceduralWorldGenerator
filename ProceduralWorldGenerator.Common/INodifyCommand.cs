using System.Windows.Input;

namespace ProceduralWorldGenerator.Common
{
    public interface INodifyCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}