using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NodeViewModelBase : ObservableObject
    {
        private string _variableName;

        public virtual string Title => _variableName;

        [JsonProperty]
        public string VariableName
        {
            get => _variableName;
            set => SetProperty(ref _variableName, value);
        }

        [JsonProperty]
        public bool SupportEdit { get; set; }
    }
}