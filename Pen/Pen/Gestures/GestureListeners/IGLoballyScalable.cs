using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Gestures.GestureListeners
{
    public interface IGloballyScalable
    {
        bool IsScaleAvailable { get; }
        void PauseScaleListener();
        void ResumeScaleListener();
        void Scale(ScaleEventArgs args);
    }
}
