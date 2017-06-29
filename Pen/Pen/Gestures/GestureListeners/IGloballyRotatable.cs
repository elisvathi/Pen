using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Gestures.GestureListeners
{
   public interface IGloballyRotatable
    {
        bool IsRoateAvailable { get; }
        void PauseRotateListener();
        void ResumeRotateListener();
        void Rotate(RotateEventArgs args);
    }
}
