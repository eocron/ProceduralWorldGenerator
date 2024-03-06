using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EditablePointViewModel : ObservableObject
    {
        private double _x;
        private double _y;
        [JsonProperty]
        public double X
        {
            get => _x;
            set => SetProperty(ref _x, value);
        }
        [JsonProperty]
        public double Y
        {
            get => _y;
            set => SetProperty(ref _y, value);
        }
        
        public EditablePointViewModel(){}

        public EditablePointViewModel(double x, double y)
        {
            _x = x;
            _y = y;
        }
    }
}