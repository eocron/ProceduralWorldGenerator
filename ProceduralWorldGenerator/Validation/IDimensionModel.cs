namespace ProceduralWorldGenerator.Validation
{
    public interface IDimensionModel
    {
        int MinDimension { get; }
        int MaxDimension { get; }
        
        int Dimension { get; set; }
    }
}