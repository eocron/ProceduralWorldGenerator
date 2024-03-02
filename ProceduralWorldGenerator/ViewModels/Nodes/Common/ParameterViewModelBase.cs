using Nodify.Shared;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    public class ParameterViewModelBase : ObservableObject
    {
        private string _title = "<none>";
        private bool _isInput;
        private int _order;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        public bool IsInput
        {
            get => _isInput;
            set => SetProperty(ref _isInput, value);
        }

        public int Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        public virtual bool CanConnect(ParameterViewModelBase other)
        {
            if (other.GetType() != GetType())
                return false;
            return true;
        }
    }
    
    public class ParameterViewModelBase<TValue> : ParameterViewModelBase
    {
        private TValue _value;
        public TValue Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}