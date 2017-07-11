using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.UI.CustomControls
{
    public class BindableValue<T>
    {
        public delegate void PropertyChanged(BindableValue<T> Sender, BindableValue<T> OriginalSender);
        public delegate void PropertyChangedExplicit(BindableValue<T> sender);
        public event PropertyChangedExplicit OnPropertyChangedExplicit;
        public event PropertyChanged OnPropertyChanged;
        public BindableValue(T val)
        {
            _value = val;
        }
        private T _value;

        public Type TypeOf => typeof(T);

        public T Value { get => _value; set => this._value = value; }

        public void RequestChange(BindableValue<T> sender, BindableValue<T> originalSender)
        {
            if (originalSender != this) { 
            _value = sender.Value;
            NotifyChanged(originalSender);
            }
        }
        public void RequestExplicitChange(T value)
        {
            _value = value;
            NotifyChanged(this);
        }
        public void Bind(BindableValue<T> binder, bool UseBinderValue = false)
        {
            binder.OnPropertyChanged += RequestChange;
            OnPropertyChanged += binder.RequestChange;
            if (UseBinderValue)
            {
                RequestChange(binder, binder);
            }
            else
            {
                binder.RequestChange(this, this);
            }
        }
        public void RemoveBinding(BindableValue<T> binder)
        {
            binder.OnPropertyChanged -= RequestChange;
            OnPropertyChanged -= binder.RequestChange;
        }
        private void NotifyChanged(BindableValue<T> originalSender)
        {
            OnPropertyChanged?.Invoke(this, originalSender);
            OnPropertyChangedExplicit?.Invoke(this);
        }

    }
}
