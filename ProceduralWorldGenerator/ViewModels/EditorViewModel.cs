using System;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EditorViewModel : ObservableObject, ISerializableViewModel
    {
        public EditorViewModel()
        {
            Calculator = new GeneratorViewModel();
        }

        [JsonProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        private GeneratorViewModel _calculator = default!;
        [JsonProperty]
        public GeneratorViewModel Calculator 
        {
            get => _calculator;
            set => SetProperty(ref _calculator, value);
        }

        private string? _name;
        [JsonProperty]
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}
