using Pen.MathExtenions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pen.UI.CustomControls.FloatSliders
{
   public class FloatSlider : Slider
    {
        BindableValue<float> value;
        public FloatSlider(float minimum, float maximum)
        {
            value = new BindableValue<float>(0);
            value.OnPropertyChangedExplicit += UpdateSlider;
            this.ValueChanged += ValueUpdated;
            Minimum = minimum;
            Maximum = maximum;
        }

        private void ValueUpdated(object sender, ValueChangedEventArgs e)
        {
            value.RequestExplicitChange(e.NewValue.ToFloat());
        }
        public void Bind(BindableValue<float> binder)
        {
            value.Bind(binder, true);
        }
        public void UnBind(BindableValue<float> binded)
        {
            value.RemoveBinding(binded);
        }

        private void UpdateSlider(BindableValue<float> sender)
        {
            ValueChanged -= ValueUpdated;
            Value = sender.Value;
            ValueChanged += ValueUpdated;
        }
        
    }
}
