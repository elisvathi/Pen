using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Drawing.Services;
using Pen.Gestures;

namespace Pen.UI.CustomControls.ColorSliders
{
    public class RedSlider : ColorControl
    {
        public RedSlider(DrawingConfigService serv, DoubleTouchGestureRecognizer dt, SingleTouchGestureRecognizer st) : base(serv, dt, st)
        {
            PropType = ColorProperty.Red;
        }
    }
}
