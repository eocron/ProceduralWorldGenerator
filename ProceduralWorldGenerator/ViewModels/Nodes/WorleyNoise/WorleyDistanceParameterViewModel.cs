using Newtonsoft.Json;
using ProceduralWorldGenerator.ViewModels.Nodes.Common;

namespace ProceduralWorldGenerator.ViewModels.Nodes.WorleyNoise
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WorleyDistanceParameterViewModel : ParameterViewModelBase<WorleyDistance>
    {
    }
}