using System;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NodeViewModelBase : ObservableObject, IIdModel
    {
        private string _variableName;
        private string _id = Guid.NewGuid().ToString();

        public virtual string Title => _variableName;

        [JsonProperty]
        public string VariableName
        {
            get => _variableName;
            set => SetProperty(ref _variableName, value);
        }

        [JsonProperty]
        public bool SupportEdit { get; set; }

        [JsonProperty]
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
    }
}