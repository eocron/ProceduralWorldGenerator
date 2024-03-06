using System.ComponentModel;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Spline
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SplineEditorViewModel : ObservableObject
    {
        private SplineEditorClamp _leftClamp;
        private SplineEditorClamp _rightClamp;
        [JsonProperty]
        public SplineEditorClamp LeftClamp
        {
            get => _leftClamp;
            set => SetProperty(ref _leftClamp, value);
        }
        [JsonProperty]
        public SplineEditorClamp RightClamp
        {
            get => _rightClamp;
            set => SetProperty(ref _rightClamp, value);
        }
        [JsonProperty]
        public BindingList<EditablePointViewModel> DataPoints { get; set; } = new BindingList<EditablePointViewModel>();
    }
}