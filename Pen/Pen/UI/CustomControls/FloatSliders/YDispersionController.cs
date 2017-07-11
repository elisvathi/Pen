using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Gestures;
using Pen.Drawing.Services;

namespace Pen.UI.CustomControls.FloatSliders
{
    public class YDispersionController : FloatController
    {
        public YDispersionController(DrawingConfigService serv, DoubleTouchGestureRecognizer dt, SingleTouchGestureRecognizer st) : base(dt, st)
        {
            minValue = 0;
            maxValue = 200;
            BindDefault(serv.YDispersion);
        }
    }
}
