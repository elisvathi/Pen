using System.ComponentModel;
using Xamarin.Forms;

namespace Pen.Gestures
{
    public class SingleTouchGestureRecognizer : IGestureRecognizer
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void TouchStarted(PTouch touch) { }
        public void TouchMoved(PTouch touch) { }
        public void TouchEnded(PTouch touch) { }
        public void TouchExited(PTouch touch) { }
        public void TouchCancelled(PTouch touch) { }

        private GestureCatcher _catcher;
        public void SetCatcher(GestureCatcher catcher)
        {
            _catcher = catcher;
            _catcher.TouchStarted += TouchStarted;
            _catcher.TouchMoved += TouchMoved;
            _catcher.TouchExited += TouchExited;
            _catcher.TouchCancelled += TouchCancelled;
            _catcher.TouchEnded += TouchEnded;
        }
    }
}
