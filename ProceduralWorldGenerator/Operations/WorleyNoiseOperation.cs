using ProceduralWorldGenerator.OperationTypes;

namespace ProceduralWorldGenerator.Operations
{
    [OperationInfo(DisplayName = "Worley noise")]
    public class WorleyNoiseOperation : NoiseOperation
    {
        [OperationTypeInfo(IsInput = true)]
        public WorleyDistanceOperationType Distance { get; set; }
        [OperationTypeInfo(IsInput = true)]
        public WorleyCombinationOperationType Combination { get; set; }
    }
}