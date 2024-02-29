using System;

namespace Nodify.Shared
{
    public abstract class ObservableWrapper<TModel> : ObservableObject
    {
        public TModel Value { get; set; }

        public virtual void ChangeValue(Action<TModel> changeAction)
        {
            changeAction(Value);
            IsDirty = true;
            OnPropertyChanged(nameof(Value));
        }
    }
}