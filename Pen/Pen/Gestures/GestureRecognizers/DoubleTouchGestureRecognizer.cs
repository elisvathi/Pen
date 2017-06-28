using System.ComponentModel;
using Xamarin.Forms;

namespace Pen.Gestures
{
    public class DoubleTouchGestureRecognizer : IGestureRecognizer
    {

        private GestureCatcher _catcher;

        public event PropertyChangedEventHandler PropertyChanged;
        public void DoubleTouchStarted(DoubleTouchEventArgs args) { }
        public void OnRotate(RotateEventArgs args) { }
        public void OnScale(ScaleEventArgs args) { }
        public void OnMove(MoveEventArgs args) { }
        public void DoubleTouchEnded(DoubleTouchEventArgs args) { }
        public void SetCatcher(GestureCatcher catcher)
        {
            _catcher = catcher;
            _catcher.DoubleTouchStarted += DoubleTouchStarted;
            _catcher.DoubleTouchEnded += DoubleTouchEnded;
            _catcher.MoveEvent += OnMove;
            _catcher.ScaleEvent += OnScale;
            _catcher.RotateEvent += OnRotate;
        }
    }
}
