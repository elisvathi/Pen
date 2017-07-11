using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Gestures;
using Pen.MathExtenions;
using Pen.Drawing.Services;

namespace Pen.UI.CustomControls
{
    public class StrokeWidthController : FloatController
    {
        public StrokeWidthController(DrawingConfigService serv, DoubleTouchGestureRecognizer dt, SingleTouchGestureRecognizer st) : base( dt, st)
        {
            minValue = 0.1F;
            maxValue = 200;
            BindDefault(serv.S_Width);
        }
    }
}
