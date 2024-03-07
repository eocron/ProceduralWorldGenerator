using System;
using System.Collections.ObjectModel;

namespace ProceduralWorldGenerator.Common
{
    public static class ObservableCollectionHelper
    {
        public static void Resize<T>(this ObservableCollection<T> collection, int newSize, Func<int, T> valueProvider)
        {
            var diff = collection.Count - newSize;
            if (diff > 0)
                for (var i = 0; i < diff; i++)
                    collection.RemoveAt(collection.Count - 1);
            else
                for (var i = 0; i < -diff; i++)
                    collection.Add(valueProvider(collection.Count));
        }

        public static void Resize<T>(this NodifyObservableCollection<T> collection, int newSize,
            Func<int, T> valueProvider)
        {
            var diff = collection.Count - newSize;
            if (diff > 0)
                for (var i = 0; i < diff; i++)
                    collection.RemoveAt(collection.Count - 1);
            else
                for (var i = 0; i < -diff; i++)
                    collection.Add(valueProvider(collection.Count));
        }
    }
}