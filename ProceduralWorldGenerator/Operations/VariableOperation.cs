using ProceduralWorldGenerator.OperationTypes;

namespace ProceduralWorldGenerator.Operations
{
    [OperationInfo(DisplayName = "constant")]
    public class VariableOperation : IOperation
    {
        [OperationTypeInfo(IsOutput = true)]
        public VectorOperationType Value { get; set; } = new VectorOperationType(1);
    }
}