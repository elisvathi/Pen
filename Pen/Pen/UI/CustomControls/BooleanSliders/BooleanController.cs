using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace Pen.UI.CustomControls.BooleanSliders
{
   public class BooleanController : Switch
    {
        private BindableValue<bool> value;
        public BooleanController()
        {
            value = new BindableValue<bool>(false);
            value.OnPropertyChangedExplicit += UpdateSwitch;
            this.Toggled += ValueUpdated;
        }
        protected void BindDefault(BindableValue<bool> b)
        {
            value.Bind(b, true);
        }
        private void ValueUpdated(object sender, ToggledEventArgs e)
        {
            value.RequestExplicitChange(e.Value);
        }

        private void UpdateSwitch(BindableValue<bool> Sender)
        {
            this.IsToggled = Sender.Value;
        }
    }
}
