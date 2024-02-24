using ProceduralWorldGenerator.OperationTypes;

namespace ProceduralWorldGenerator.Operations
{
    [OperationInfo(DisplayName = "Permutation table", IsRuntimeInput = true)]
    public class PermutationTableOperation : IOperation
    {
        [OperationTypeInfo(IsOutput = true)]
        public PermutationTableOperationType Permutation { get; set; } = new PermutationTableOperationType();
    }
}