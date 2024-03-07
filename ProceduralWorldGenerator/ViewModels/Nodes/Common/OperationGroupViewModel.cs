using System.Windows;
using Newtonsoft.Json;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OperationGroupViewModel : GeneratorNodeViewModel
    {
        [JsonProperty]
        public Size GroupSize
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        private Size _size;
    }
}