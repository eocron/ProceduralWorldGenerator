using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NodeSyntaxViewModel : ObservableObject
    {
        private int _lastUsedDimension = 3;
        
        [JsonProperty]
        public ObservableCollection<string> UsedVariableNames { get; set; } = new ObservableCollection<string>();
        [JsonProperty]
        public int LastUsedDimension
        {
            get => _lastUsedDimension;
            set => SetProperty(ref _lastUsedDimension, value);
        }

        public void AddVariableName(string name)
        {
            if (!UsedVariableNames.Contains(name))
            {
                UsedVariableNames.Add(name);
            }
        }

        public void DeleteVariableName(string name)
        {
            while (UsedVariableNames.Remove(name))
            {
            }
        }
    }
}