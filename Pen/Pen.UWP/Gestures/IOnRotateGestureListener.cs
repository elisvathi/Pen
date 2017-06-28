using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.UWP.Gestures
{
    interface IOnRotateGestureListener
    {
        bool OnRotateBegin(RotateGestureDetector detector);
        void OnRotate(RotateGestureDetector detector);
        void OnRotateEnd(RotateGestureDetector detector);
    }
}
