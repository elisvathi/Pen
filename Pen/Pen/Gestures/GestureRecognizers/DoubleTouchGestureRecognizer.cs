using System.ComponentModel;
using Xamarin.Forms;
using static Pen.Gestures.GestureCatcher;

namespace Pen.Gestures
{
    public class DoubleTouchGestureRecognizer : IGestureRecognizer
    {

        private GestureCatcher _catcher;

        public event PropertyChangedEventHandler PropertyChanged;

        public event DoubleTouchDelegate TransformGestureStarted;
        public event DoubleTouchDelegate TransfromGestureEnded;
        public event RotateDelegate RotateTransform;
        public event ScaleDelegate ScaleTransform;
        public event MoveDelegate MoveTransform;
        

        public void DoubleTouchStarted(DoubleTouchEventArgs args) { TransformGestureStarted?.Invoke(args); }
        public void OnRotate(RotateEventArgs args) { RotateTransform?.Invoke(args); }
        public void OnScale(ScaleEventArgs args) { ScaleTransform?.Invoke(args); }
        public void OnMove(MoveEventArgs args) { MoveTransform?.Invoke(args); }
        public void DoubleTouchEnded(DoubleTouchEventArgs args) { TransfromGestureEnded?.Invoke(args); }
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
