using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Gestures.GestureListeners
{
    public interface IGloballyMovable
    {
        bool IsMoveAvailable { get; }
        void PauseMoveListener();
        void ResumeMoveListener();
        void Move(MoveEventArgs args);
    }
}
