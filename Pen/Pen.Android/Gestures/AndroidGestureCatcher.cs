using Android.Views;
using Pen.Gestures;
using Pen.Geometry;

namespace Pen.Droid.Gestures
{
    public class AndroidGestureCatcher : GestureCatcher
    {
        int FirstID;
        int SecondID;
        public AndroidGestureCatcher()
        {
            FirstID = -1;
            SecondID = -1;
        }
        private bool IsDoubleTouch
        {
            get { return FirstID != -1 && SecondID != -1; }
        }
        private bool IsSingleTouch(MotionEvent e)
        {
            return !IsDoubleTouch && e.GetPointerId(e.ActionIndex) == FirstID;
        }
        private void UpdateFirstPoint(MotionEvent e)
        {
            newFirstPoint = new PVector(e.RawX, e.RawY);
            newFirstPRessure = e.Pressure;
            newFirstTime = (ulong)e.EventTime;
        }
        private void UpdateSecondPoint(MotionEvent e)
        {
            newSecondPoint = new PVector(e.RawX, e.RawY);
            newSecondPressure = e.Pressure;
            newSecondTime = (ulong)e.EventTime;
        }
        private void FinishDoubleTouch(MotionEvent e)
        {
            var id = e.GetPointerId(e.ActionIndex);
            if (id == FirstID) { FirstID = SecondID; SecondID = -1; OnDoubleTouchEnded(); } else if (id == SecondID) { SecondID = -1; OnDoubleTouchEnded(); }
        }
        internal void OnMotion(object sender, View.GenericMotionEventArgs e)
        {
            var id = e.Event.GetPointerId(e.Event.ActionIndex);
            switch (e.Event.ActionMasked)
            {

                case MotionEventActions.Outside:
                    {
                        if (!IsDoubleTouch)
                        {
                            if (!IsSingleTouch(e.Event))
                            {
                                SingleTouchExited();
                            }
                        }
                        else
                        {
                            FinishDoubleTouch(e.Event);
                        }
                        break;
                    }
                case MotionEventActions.PointerDown:
                    {
                        if (FirstID == -1)
                        {
                            FirstID = id;
                            UpdateFirstPoint(e.Event);
                            SingleTouchStart();

                        }
                        else if (SecondID == -1)
                        {
                            SecondID = id;
                            UpdateSecondPoint(e.Event);
                            SingleTouchEnded();
                            OnDoubleTouchStarted();
                        }
                        break;
                    }
                case MotionEventActions.PointerUp:
                    {
                        if (IsDoubleTouch)
                        {
                            FinishDoubleTouch(e.Event);
                        }
                        else if (IsSingleTouch(e.Event))
                        {
                            SingleTouchEnded();
                        }
                        break;
                    }
                case MotionEventActions.Cancel:
                    {
                        if (IsDoubleTouch)
                        {
                            FinishDoubleTouch(e.Event);
                        }
                        else if (IsSingleTouch(e.Event))
                        {
                            SingleTouchCancelled();
                        }
                        break;
                    }
                case MotionEventActions.Move:
                    {
                        if (IsDoubleTouch)
                        {
                            if (id == FirstID)
                            {
                                SwapOldNew();
                                UpdateFirstPoint(e.Event);
                                OnScale();
                                OnRotate();
                                OnMove();
                            }
                            else if (id == SecondID)
                            {
                                SwapOldNew();
                                UpdateSecondPoint(e.Event);
                                OnScale();
                                OnRotate();
                                OnMove();
                            }
                        }
                        else if (IsSingleTouch(e.Event))
                        {
                            SwapOldNew();
                            UpdateFirstPoint(e.Event);
                            SingleTouchMoved();
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}