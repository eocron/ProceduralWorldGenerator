using ProceduralWorldGenerator.OperationTypes;

namespace ProceduralWorldGenerator.Operations
{
    [OperationInfo(DisplayName = "Chunk", IsRuntimeInput = true)]
    public class ChunkOperation : IOperation
    {
        [OperationTypeInfo(IsOutput = true, DisplayName = "position")]
        public VectorOperationType Value { get; set; }
        
        [OperationTypeInfo(IsOutput = true, DisplayName = "chunk size")]
        public VectorOperationType Size { get; set; }
    }
}