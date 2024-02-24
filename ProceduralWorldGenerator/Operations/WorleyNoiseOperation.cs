using ProceduralWorldGenerator.OperationTypes;
using ProceduralWorldGenerator.OperationTypes.WorleyNoise;

namespace ProceduralWorldGenerator.Operations
{
    [OperationInfo(DisplayName = "Worley noise")]
    public class WorleyNoiseOperation : NoiseOperation
    {
        [OperationTypeInfo(IsInput = true)]
        public WorleyDistanceOperationType Distance { get; set; } = new WorleyDistanceOperationType();
        [OperationTypeInfo(IsInput = true)]
        public WorleyCombinationOperationType Combination { get; set; } = new WorleyCombinationOperationType();
    }
}