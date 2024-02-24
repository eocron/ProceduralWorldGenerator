using ProceduralWorldGenerator.OperationTypes;

namespace ProceduralWorldGenerator.Operations
{
    [OperationInfo(DisplayName = "Chunk", IsRuntimeInput = true)]
    public class ChunkOperation : IOperation
    {
        [OperationTypeInfo(IsOutput = true)]
        public VectorOperationType Value { get; set; } = new VectorOperationType("position");

        [OperationTypeInfo(IsOutput = true)]
        public VectorOperationType Size { get; set; } = new VectorOperationType("size");
    }
}