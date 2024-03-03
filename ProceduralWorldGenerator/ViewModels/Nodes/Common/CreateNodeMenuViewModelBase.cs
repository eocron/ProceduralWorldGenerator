using System.ComponentModel;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public abstract class CreateNodeMenuViewModelBase<TNodeViewModel> : CreateMenuViewModelBase
        where TNodeViewModel : NodeViewModelBase
    {
        public TNodeViewModel PrevNodeViewModel
        {
            get => (TNodeViewModel)PrevModel;
            set
            {
                PrevModel = value;
                OnPropertyChanged();
            }
        }
        public TNodeViewModel NodeViewModel
        {
            get => (TNodeViewModel)NewModel;
            set
            {
                NewModel = value;
                OnPropertyChanged();
            }
        }

        public CreateNodeMenuViewModelBase()
        {
            PropertyChanged += Changed;
        }

        private void Changed(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NewModel))
            {
                OnPropertyChanged(nameof(NodeViewModel));
            }
            else if (e.PropertyName == nameof(PrevModel))
            {
                OnPropertyChanged(nameof(PrevNodeViewModel));
            }
        }
    }
}