using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Gestures;
using Pen.MathExtenions;

namespace Pen.UI.CustomControls
{
    public abstract class FloatController : SliderController<float>
    {
       protected float minValue;
       protected float maxValue;
       
        public FloatController(DoubleTouchGestureRecognizer dt, SingleTouchGestureRecognizer st) : base( dt, st)
        {
            InitializeValue(0);
        }
        protected void BindDefault(BindableValue<float> val)
        {
            BindProperty(val, 0);
        }
        protected override void OnValueChanged(BindableValue<float> Sender)
        {
            UpdateValue(NormalValue(Sender.Value));
        }

        protected override void UpdateProperty(float requestValue, BindableValue<float> ojb)
        {
            ojb.RequestExplicitChange(RealValue(requestValue));
        }

        private float RealValue(float requestValue)
        {
           return requestValue.ToDouble().Map(0, 1, minValue, maxValue).ToFloat();
        }
        private float NormalValue(float val)
        {
            return val.ToDouble().Map(minValue, maxValue, 0, 1).ToFloat();
        }
        protected override void UpdateProperty(float requestValue)
        {
            UpdateProperty(requestValue, Properties[0]);
        }
    }
}
