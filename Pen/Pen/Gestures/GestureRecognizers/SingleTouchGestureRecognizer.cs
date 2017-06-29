using System.ComponentModel;
using Xamarin.Forms;
using static Pen.Gestures.GestureCatcher;

namespace Pen.Gestures
{
    public class SingleTouchGestureRecognizer : IGestureRecognizer
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public event SingleTouchDelegate StartedTouch;
        public event SingleTouchDelegate EndedTouch;
        public event SingleTouchDelegate MovedTouch;
        public event SingleTouchDelegate ExitedTouch;
        public event SingleTouchDelegate CancelledTouch;

        public void TouchStarted(PTouch touch) { StartedTouch?.Invoke(touch); }
        public void TouchMoved(PTouch touch) { MovedTouch?.Invoke(touch); }
        public void TouchEnded(PTouch touch) { EndedTouch?.Invoke(touch); }
        public void TouchExited(PTouch touch) { ExitedTouch?.Invoke(touch); }
        public void TouchCancelled(PTouch touch) { CancelledTouch?.Invoke(touch); }

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
