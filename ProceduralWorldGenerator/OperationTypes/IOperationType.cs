namespace ProceduralWorldGenerator.OperationTypes
{
    public interface IOperationType
    {
        bool IsAssignableFrom(IOperationType operationType);
    }
}