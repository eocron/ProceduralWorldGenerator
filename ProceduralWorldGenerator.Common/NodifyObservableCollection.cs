using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ProceduralWorldGenerator.Common
{
    public class NodifyObservableCollection<T> : Collection<T>, INodifyObservableCollection<T>, INotifyPropertyChanged,
        INotifyCollectionChanged
    {
        public NodifyObservableCollection()
        {
        }

        public NodifyObservableCollection(IEnumerable<T> collection)
            : base(new List<T>(collection))
        {
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected static readonly NotifyCollectionChangedEventArgs ResetCollectionChanged =
            new(NotifyCollectionChangedAction.Reset);

        protected static readonly PropertyChangedEventArgs IndexerPropertyChanged = new("Item[]");
        protected static readonly PropertyChangedEventArgs CountPropertyChanged = new("Count");
        private readonly List<Action<IList<T>>> _cleared = new();

        private readonly List<Action<T>> _added = new();
        private readonly List<Action<T>> _removed = new();

        #region Collection Events

        public INodifyObservableCollection<T> WhenAdded(Action<T> added)
        {
            if (added != null) _added.Add(added);
            return this;
        }

        public INodifyObservableCollection<T> WhenRemoved(Action<T> removed)
        {
            if (removed != null) _removed.Add(removed);
            return this;
        }

        public INodifyObservableCollection<T> WhenCleared(Action<IList<T>> cleared)
        {
            if (cleared != null) _cleared.Add(cleared);
            return this;
        }

        protected virtual void NotifyOnItemAdded(T item)
        {
            for (var i = 0; i < _added.Count; i++) _added[i](item);
        }

        protected virtual void NotifyOnItemRemoved(T item)
        {
            for (var i = 0; i < _removed.Count; i++) _removed[i](item);
        }

        protected virtual void NotifyOnItemsCleared(IList<T> items)
        {
            for (var i = 0; i < _cleared.Count; i++) _cleared[i](items);
        }

        #endregion

        #region Collection Handlers

        protected override void ClearItems()
        {
            var items = new List<T>(this);
            base.ClearItems();

            if (_cleared.Count > 0)
                NotifyOnItemsCleared(items);
            else
                for (var i = 0; i < items.Count; i++)
                    NotifyOnItemRemoved(items[i]);

            OnPropertyChanged(CountPropertyChanged);
            OnPropertyChanged(IndexerPropertyChanged);
            OnCollectionChanged(ResetCollectionChanged);
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);

            OnPropertyChanged(CountPropertyChanged);
            OnPropertyChanged(IndexerPropertyChanged);
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
            NotifyOnItemAdded(item);
        }

        protected override void RemoveItem(int index)
        {
            var item = base[index];
            base.RemoveItem(index);

            OnPropertyChanged(CountPropertyChanged);
            OnPropertyChanged(IndexerPropertyChanged);
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
            NotifyOnItemRemoved(item);
        }

        protected override void SetItem(int index, T item)
        {
            var prev = base[index];
            base.SetItem(index, item);
            OnPropertyChanged(IndexerPropertyChanged);
            OnCollectionChanged(NotifyCollectionChangedAction.Replace, prev, item, index);
            NotifyOnItemRemoved(prev);
            NotifyOnItemAdded(item);
        }

        public void Move(int oldIndex, int newIndex)
        {
            var prev = base[oldIndex];
            base.RemoveItem(oldIndex);
            base.InsertItem(newIndex, prev);
            OnPropertyChanged(IndexerPropertyChanged);
            OnCollectionChanged(NotifyCollectionChangedAction.Move, prev, newIndex, oldIndex);
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object? item, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object? item, int index, int oldIndex)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object? oldItem, object? newItem,
            int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }

        #endregion
    }
}