using Pen.Geometry;


namespace Pen.Gestures
{
    public class GestureCatcher
    {
        #region Constructor
        public GestureCatcher() { }
        #endregion

        #region Delegates
        public delegate void DoubleTouchDelegate(DoubleTouchEventArgs args);
        public delegate void ScaleDelegate(ScaleEventArgs args);
        public delegate void MoveDelegate(MoveEventArgs args);
        public delegate void RotateDelegate(RotateEventArgs args);
        public delegate void SingleTouchDelegate(PTouch args);
        #endregion

        #region Events
        public event DoubleTouchDelegate DoubleTouchStarted;
        public event DoubleTouchDelegate DoubleTouchEnded;
        public event RotateDelegate RotateEvent;
        public event ScaleDelegate ScaleEvent;
        public event MoveDelegate MoveEvent;
        public event SingleTouchDelegate TouchStarted;
        public event SingleTouchDelegate TouchMoved;
        public event SingleTouchDelegate TouchEnded;
        public event SingleTouchDelegate TouchCancelled;
        public event SingleTouchDelegate TouchExited;
        #endregion

        #region EventInvokers
        protected void SingleTouchStart()
        {
            TouchStarted?.Invoke(FirstTouch);
        }
        protected void SingleTouchEnded()
        {
            TouchEnded?.Invoke(FirstTouch);
        }
        protected void SingleTouchMoved()
        {
            TouchMoved?.Invoke(FirstTouch);
        }
        protected void SingleTouchCancelled()
        {
            TouchCancelled?.Invoke(FirstTouch);
        }
        protected void SingleTouchExited()
        {
            TouchExited?.Invoke(FirstTouch);
        }
        protected void OnDoubleTouchStarted()
        {
            DoubleTouchStarted?.Invoke(DoubleTouchArgs);
        }
        protected void OnDoubleTouchEnded()
        {
            DoubleTouchEnded?.Invoke(DoubleTouchArgs);
        }
        protected void OnRotate()
        {
            RotateEvent?.Invoke(RotateArgs);
        }
        protected void OnScale()
        {
            ScaleEvent?.Invoke(ScaleArgs);
        }
        protected void OnMove()
        {
            MoveEvent.Invoke(MoveArgs);
        }
        #endregion

        #region Protected Properties and Methods
        protected PVector oldFirstPoint, oldSecondPoint;
        protected PVector newFirstPoint, newSecondPoint;
        protected double oldFirstPressure, oldSecondPressure;
        protected double newFirstPRessure, newSecondPressure;
        protected ulong oldFirstTime, oldSecondTime, newFirstTime, newSecondTime;
        protected void SwapOldNew()
        {
            oldFirstPoint = newFirstPoint;
            oldSecondPoint = newSecondPoint;
            oldFirstPressure = newFirstPRessure;
            oldSecondPressure = newSecondPressure;
            oldFirstTime = newFirstTime;
            oldSecondTime = newSecondTime;
        }
        #endregion

        #region EventArgsProperties
        public PTouch FirstTouch
        {
            get
            {
                return new PTouch()
                {
                    Position = newFirstPoint,
                    Pressure = newFirstPRessure,
                    Time = newFirstTime
                };
            }
        }
        public PTouch SecondTouch
        {
            get
            {
                return new PTouch()
                {
                    Position = newSecondPoint,
                    Pressure = newSecondPressure,
                    Time = newSecondTime
                };
            }
        }
        private DoubleTouchEventArgs DoubleTouchArgs
        {
            get
            {
                return new DoubleTouchEventArgs()
                {
                    Touch1 = FirstTouch,
                    Touch2 = SecondTouch
                };
            }
        }
        private RotateEventArgs RotateArgs { get { return new RotateEventArgs() { Center = NewCenter, Angle = AngleDegrees }; } }
        private ScaleEventArgs ScaleArgs { get { return new ScaleEventArgs() { Center = NewCenter, Value = Scale }; } }
        private MoveEventArgs MoveArgs { get { return new MoveEventArgs() { Center = NewCenter, Displacement = MovementVector }; } }

        #endregion

        #region OtherPrivateProperties
        private static PVector GetCenter(PVector p1, PVector p2)
        {
            var vec = PVector.Sub(p2, p1);
            vec.Mult(0.5);
            vec.Add(p1);
            return vec;
        }

        private PVector NewCenter
        {
            get
            {
                return GetCenter(newFirstPoint, newSecondPoint);
            }
        }

        private PVector OldCenter
        {
            get
            {
                return GetCenter(oldFirstPoint, oldSecondPoint);
            }
        }

        private PVector MovementVector
        {
            get
            {
                return PVector.Sub(NewCenter, OldCenter);
            }
        }

        private double Scale
        {
            get
            {
                return PVector.DistanceBetween(newFirstPoint, newSecondPoint) / PVector.DistanceBetween(oldFirstPoint, oldSecondPoint);
            }
        }

        private double AngleDegrees
        {
            get
            {
                PVector oldvec = PVector.Sub(oldSecondPoint, oldFirstPoint);
                PVector newvec = PVector.Sub(newSecondPoint, newFirstPoint);
                var ang =  PVector.AngleBetweenDegrees(newvec, oldvec);
                
                return ang;
            }
        }
        #endregion OtherPrivateProperties


    }
}
