using System.Collections.ObjectModel;
using Newtonsoft.Json;
using ProceduralWorldGenerator.Common;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.Expression
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ExpressionNodeViewModel : NodeViewModelBase
    {
        [JsonProperty]
        public int InputDimension
        {
            get => Input.Dimension;
            set
            {
                SetNestedProperty(nameof(Input), Input.Dimension, value, () => Input.Dimension = value);
                OnPropertyChanged();
            }
        }

        [JsonProperty]
        public int OutputDimension
        {
            get => Output.Dimension;
            set
            {
                TransformExpressions.Resize(value, _ => "");
                SetNestedProperty(nameof(Output), Output.Dimension, value, () => Output.Dimension = value);
                OnPropertyChanged();
            }
        }

        [JsonProperty] public ObservableCollection<Wrapper<string>> TransformExpressions { get; set; } = new();

        [JsonProperty] public VectorParameterViewModel Input { get; set; } = new() { IsInput = true };

        [JsonProperty] public VectorParameterViewModel Output { get; set; } = new();
    }
}