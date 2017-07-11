using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Gestures;
using SkiaSharp;

namespace Pen.UI.CustomControls
{
    public abstract class SliderController<T> : PRoundSlider
    {
        protected List<BindableValue<T>> Properties;
        protected List<T> Value
        {
            get
            {
                var l = new List<T>();
                foreach (var t in Properties)
                {
                    l.Add(t.Value);
                }
                return l;
            }
        }
        public SliderController(DoubleTouchGestureRecognizer dt, SingleTouchGestureRecognizer st) : base(dt, st)
        {
            this.SliderValueChanged += UpdateProperty;
        }
        protected abstract void UpdateProperty(float requestValue, BindableValue<T> ojb);
        protected abstract void UpdateProperty(float requestValue);
        protected abstract void OnValueChanged(BindableValue<T> Sender);

        protected virtual void InitializeValue(T val)
        {
            if (Properties == null) { Properties = new List<BindableValue<T>>(); }
            Properties.Add(new BindableValue<T>(val));
            Properties[Properties.Count - 1].OnPropertyChangedExplicit += OnValueChanged;
        }

        protected virtual void BindProperty(BindableValue<T> b, int i)
        {
            Properties[i].Bind(b, true);
        }
        
    }
}
