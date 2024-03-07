namespace ProceduralWorldGenerator.Common
{
    public delegate bool ValidateValueCallback<in TValue>(TValue value);
}