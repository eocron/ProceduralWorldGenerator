using System;

namespace ProceduralWorldGenerator.Common
{
    public delegate Boolean ValidateValueCallback<in TValue>( TValue value );
}