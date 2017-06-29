using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Gestures.GestureListeners
{
    public interface ISingleTouchListener
    {
        bool IsListening { get; }
        void PauseSingleTouch();
        void ResumeSingleTouch();
        void TouchStart(PTouch t);
        void TouchEnd(PTouch t);
        void TouchMove(PTouch t);
        void TouchCancel(PTouch t);
        void TouchExit(PTouch t);

    }
}
