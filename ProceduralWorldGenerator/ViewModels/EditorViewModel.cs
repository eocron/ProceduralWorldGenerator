using System;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EditorViewModel : ObservableObject, IIdModel
    {
        public EditorViewModel()
        {
            Calculator = new GeneratorViewModel();
        }

        [JsonProperty]
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private GeneratorViewModel _calculator = default!;
        [JsonProperty]
        public GeneratorViewModel Calculator 
        {
            get => _calculator;
            set => SetProperty(ref _calculator, value);
        }

        private string? _name;
        private string _id = Guid.NewGuid().ToString();

        [JsonProperty]
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}
