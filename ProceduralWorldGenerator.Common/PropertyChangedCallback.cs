using System.Windows;

namespace ProceduralWorldGenerator.Common
{
    public delegate void PropertyChangedCallback<in TOwner>(TOwner sender, DependencyPropertyChangedEventArgs e)
        where TOwner : DependencyObject;
}