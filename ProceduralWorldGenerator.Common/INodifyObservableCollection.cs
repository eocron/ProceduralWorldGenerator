using System;
using System.Collections.Generic;

namespace ProceduralWorldGenerator.Common
{
    public interface INodifyObservableCollection<T>
    {
        /// <summary>
        ///     Called when a new item is added
        /// </summary>
        /// <param name="added">The callback to execute when an item is added</param>
        /// <returns>Returns self</returns>
        INodifyObservableCollection<T> WhenAdded(Action<T> added);

        /// <summary>
        ///     Called when the collection is cleared
        ///     NOTE: It does not call <see cref="WhenRemoved(Action{T})" /> on each item
        /// </summary>
        /// <param name="added">The callback to execute when the collection is cleared</param>
        /// <returns>Returns self</returns>
        INodifyObservableCollection<T> WhenCleared(Action<IList<T>> cleared);

        /// <summary>
        ///     Called when an existing item is removed
        ///     Note: It is not called when items are cleared if <see cref="WhenCleared(Action{IList{T}})" /> is used
        /// </summary>
        /// <param name="added">The callback to execute when an item is removed</param>
        /// <returns>Returns self</returns>
        INodifyObservableCollection<T> WhenRemoved(Action<T> removed);
    }
}