using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ParameterViewModelBase : ObservableObject
    {
        private string _title = "<none>";
        private bool _isInput;
        private int _order;

        [JsonProperty]
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        [JsonProperty]
        public bool IsInput
        {
            get => _isInput;
            set => SetProperty(ref _isInput, value);
        }

        [JsonProperty]
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
    
    [JsonObject(MemberSerialization.OptIn)]
    public class ParameterViewModelBase<TValue> : ParameterViewModelBase
    {
        private TValue _value;
        
        [JsonProperty]
        public TValue Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}