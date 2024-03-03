namespace ProceduralWorldGenerator.Common
{
    public delegate TValue CoerceValueCallback<in TOwner, TValue>( TOwner sender, TValue value );
}