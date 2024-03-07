using Newtonsoft.Json;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Common
{
    [JsonObject(MemberSerialization.OptIn)]
    public class VectorParameterViewModel : ParameterViewModelBase<float[]>, IDimensionParameter
    {
        [JsonProperty]
        public int Dimension
        {
            get => _dimension;
            set => SetProperty(ref _dimension, value);
        }

        private int _dimension;
    }
}