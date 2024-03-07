using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NodeSyntaxViewModel : ObservableObject
    {
        public void AddVariableName(string name)
        {
            if (!UsedVariableNames.Contains(name)) UsedVariableNames.Add(name);
        }

        public void DeleteVariableName(string name)
        {
            while (UsedVariableNames.Remove(name))
            {
            }
        }

        [JsonProperty]
        public int LastUsedDimension
        {
            get => _lastUsedDimension;
            set => SetProperty(ref _lastUsedDimension, value);
        }

        [JsonProperty] public ObservableCollection<string> UsedVariableNames { get; set; } = new();

        private int _lastUsedDimension = 3;
    }
}