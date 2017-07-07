using Android.Views;
using Pen.Gestures;
using Pen.Geometry;
using static Android.Views.View;

namespace Pen.Droid.Gestures
{
    public class AndroidGestureCatcher : GestureCatcher
    {
        bool isInProgress;
        bool singleInProgress;
        int FirstID;
        int SecondID;
        public AndroidGestureCatcher()
        {
            FirstID = -1;
            SecondID = -1;
        }
        private bool IsDoubleTouch
        {
            get { return isInProgress; }
        }
        private bool IsSingleTouch(MotionEvent e)
        {
            return !IsDoubleTouch && e.GetPointerId(e.ActionIndex) == FirstID && singleInProgress;
        }
        private void UpdateFirstPoint(MotionEvent e, int index)
        {
            newFirstPoint = new PVector(e.GetX(index), e.GetY(index));
            newFirstPRessure = e.GetPressure(index);
            newFirstTime = (ulong)e.EventTime;
        }
        private void UpdateSecondPoint(MotionEvent e, int index)
        {
            newSecondPoint = new PVector(e.GetX(index), e.GetY(index));
            newSecondPressure = e.GetPressure(index);
            newSecondTime = (ulong)e.EventTime;
        }
        private void FinishDoubleTouch(MotionEvent e)
        {
            var id = e.GetPointerId(e.ActionIndex);
            if (id == FirstID) { FirstID = SecondID; SecondID = -1; OnDoubleTouchEnded(); isInProgress = false; } else if (id == SecondID) { SecondID = -1; OnDoubleTouchEnded(); isInProgress = false; }
        }
        internal void OnMotion(MotionEvent e)
        {
            var id = e.GetPointerId(e.ActionIndex);
            switch (e.ActionMasked)
            {

                case MotionEventActions.Down:
                    {
                        FirstID = id;
                        SecondID = -1;
                        isInProgress = false;
                        singleInProgress = true;
                        var index = e.FindPointerIndex(FirstID);
                        UpdateFirstPoint(e, index);
                        SingleTouchStart();
                        break;
                    }
                case MotionEventActions.PointerDown:
                    {
                        if (FirstID != -1 && SecondID == -1)
                        {
                            SecondID = id;
                            var index1 = e.FindPointerIndex(FirstID);
                            var index2 = e.FindPointerIndex(SecondID);
                            UpdateFirstPoint(e, index1);
                            UpdateSecondPoint(e, index2);
                            SwapOldNew();
                            isInProgress = true;
                            singleInProgress = false;
                            OnDoubleTouchStarted();
                            SingleTouchEnded();
                        }
                        break;
                    }
                case MotionEventActions.Move:
                    {
                        if (IsDoubleTouch)
                        {
                            var index1 = e.FindPointerIndex(FirstID);
                            var index2 = e.FindPointerIndex(SecondID);
                            SwapOldNew();
                            UpdateFirstPoint(e, index1);
                            UpdateSecondPoint(e, index2);
                            OnMove();
                            OnRotate();
                            OnScale();
                        }else if (IsSingleTouch(e))
                        {
                            SwapOldNew();
                            UpdateFirstPoint(e, e.FindPointerIndex(FirstID));
                            SingleTouchMoved();
                        }else if (FirstID != -1)
                        {
                            FirstID = -1;
                        }
                        break;
                    }

                case MotionEventActions.Up:
                case MotionEventActions.PointerUp:
                    {
                        if (IsDoubleTouch)
                        {
                            FinishDoubleTouch(e);
                        }else if (IsSingleTouch(e))
                        {
                            SingleTouchEnded();
                            FirstID = -1;
                            singleInProgress = false;
                        }else if (FirstID != -1)
                        {
                            FirstID = -1;
                        }
                        break;
                    }
                case MotionEventActions.Cancel:
                    {

                        if (IsDoubleTouch)
                        {
                            FinishDoubleTouch(e);
                        }
                        else if (IsSingleTouch(e))
                        {
                            FirstID = -1;
                            SingleTouchCancelled();
                            singleInProgress = false;
                        }else if(FirstID != -1)
                        {
                            FirstID = -1;
                        }
                        break;
                    }
                case MotionEventActions.Outside:
                    {
                        
                        if (!IsDoubleTouch)
                        {
                            if (!IsSingleTouch(e))
                            {
                                SingleTouchExited();
                            }
                            if(FirstID != -1)
                            {
                                FirstID = -1;
                            }
                        }
                        else
                        {
                            FinishDoubleTouch(e);
                            FirstID = -1;
                            singleInProgress = false;
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