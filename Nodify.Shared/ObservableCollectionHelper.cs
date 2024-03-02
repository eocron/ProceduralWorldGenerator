using System.Collections.ObjectModel;

namespace Nodify.Shared
{
    public static class ObservableCollectionHelper
    {
        public static void Resize<T>(this ObservableCollection<T> collection, int newSize, T defaultValue = default)
        {
            var diff = collection.Count - newSize;
            if (diff > 0)
            {
                for (int i = 0; i < diff; i++)
                {
                    collection.RemoveAt(collection.Count - 1);
                }
            }
            else
            {
                for (int i = 0; i < -diff; i++)
                {
                    collection.Add(defaultValue);
                }
            }
        }
    }
}