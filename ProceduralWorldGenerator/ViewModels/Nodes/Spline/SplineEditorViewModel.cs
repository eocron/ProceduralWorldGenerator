using System.ComponentModel;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Spline
{
    public class SplineEditorViewModel : ObservableObject
    {
        private SplineEditorClamp _leftClamp;
        private SplineEditorClamp _rightClamp;

        public SplineEditorClamp LeftClamp
        {
            get => _leftClamp;
            set => SetProperty(ref _leftClamp, value);
        }

        public SplineEditorClamp RightClamp
        {
            get => _rightClamp;
            set => SetProperty(ref _rightClamp, value);
        }

        public BindingList<EditablePointViewModel> DataPoints { get; set; } = new BindingList<EditablePointViewModel>();
    }
}