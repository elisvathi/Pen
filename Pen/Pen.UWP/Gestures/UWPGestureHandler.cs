using Pen.Geometry;
using Pen.Gestures;
using Windows.UI.Xaml.Input;

namespace Pen.UWP.Gestures
{
    public class UWPGestureHandler : GestureCatcher
    {
        int firstId, secondId;
        public UWPGestureHandler()
        {
            firstId = -1;
            secondId = -1;
        }
        private void UpdateFirstPoint(PointerRoutedEventArgs e)
        {
            newFirstPoint = new PVector(e.GetCurrentPoint(null).RawPosition.X, e.GetCurrentPoint(null).RawPosition.Y);
            newFirstPRessure = e.GetCurrentPoint(null).Properties.Pressure;
            newFirstTime = e.GetCurrentPoint(null).Timestamp;
        }
        private void UpdateSecondPoint(PointerRoutedEventArgs e)
        {
            newSecondPoint = new PVector(e.GetCurrentPoint(null).RawPosition.X, e.GetCurrentPoint(null).RawPosition.Y);
            newSecondPressure = e.GetCurrentPoint(null).Properties.Pressure;
            newSecondTime = e.GetCurrentPoint(null).Timestamp;
        }
        private bool IsDoubleTouch
        {
            get
            {
                return firstId != -1 && secondId != -1;
            }
        }
        private bool IsSingleTouch(PointerRoutedEventArgs e)
        {
            return !IsDoubleTouch && (int)e.Pointer.PointerId == firstId;
        }
        public void PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (firstId == -1)
            {
                firstId = (int)e.Pointer.PointerId;
                UpdateFirstPoint(e);
                SingleTouchStart();
            }
            else if (secondId == -1)
            {
                secondId = (int)e.Pointer.PointerId;
                UpdateSecondPoint(e);
                SingleTouchEnded();
                OnDoubleTouchStarted();
            }
        }
        public void PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var id = (int)e.Pointer.PointerId;
            if (!IsDoubleTouch)
            {
                if (IsSingleTouch(e))
                {
                    SingleTouchEnded(); 
                }
            }
            else
            {

                if (id == firstId) { firstId = secondId; secondId = -1; OnDoubleTouchEnded(); }
                else if (id == secondId) { secondId = -1; OnDoubleTouchEnded(); }

            }

        }
        public void PointerExited(object sender, PointerRoutedEventArgs e) {
            if (!IsDoubleTouch)
            {
                if (IsSingleTouch(e)) { 
                SingleTouchExited();
                }
            }
            else
            {
                PointerReleased(sender, e);
            }
        }
        public void PointerCancelled(object sender, PointerRoutedEventArgs e) {
            if (!IsDoubleTouch)
            {

                if (IsSingleTouch(e))
                {
                    SingleTouchCancelled(); 
                }
            }
            else
            {
                PointerReleased(sender, e);
            }
        }
        public void PointerMoved(object sender, PointerRoutedEventArgs e) {
            var id = (int)e.Pointer.PointerId;
            if (!IsDoubleTouch)
            {
                if (IsSingleTouch(e)) {
                    SwapOldNew();
                    UpdateFirstPoint(e);
                    SingleTouchMoved();
                }
            }
            else
            {
                if (id == firstId)
                {
                    SwapOldNew();
                    UpdateFirstPoint(e);
                    OnRotate();
                    OnMove();
                    OnScale();
                }else if (id == secondId)
                {
                    SwapOldNew();
                    UpdateSecondPoint(e);
                    OnRotate();
                    OnMove();
                    OnScale();
                }
            }
        }
        
    }
}
