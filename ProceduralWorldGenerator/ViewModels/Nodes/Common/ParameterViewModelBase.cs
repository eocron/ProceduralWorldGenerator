using System;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ParameterViewModelBase : ObservableObject, IIdModel
    {
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

        [JsonProperty]
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [JsonProperty]
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isInput;
        private int _order;
        private string _id = Guid.NewGuid().ToString();
        private string _title;
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ParameterViewModelBase<TValue> : ParameterViewModelBase
    {
        [JsonProperty]
        public TValue Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private TValue _value;
    }
}