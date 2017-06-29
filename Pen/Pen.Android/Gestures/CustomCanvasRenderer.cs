using SkiaSharp.Views.Forms;
using Xamarin.Forms.Platform.Android;
using Pen.Gestures;
using Xamarin.Forms;
using Pen.Droid.Gestures;
using Pen.UI.MainCanvas;
using Android.Views;
using System;

[assembly: ExportRenderer(typeof(TouchCanvas), typeof(AndroidCanvasRenderer))]
namespace Pen.Droid.Gestures
{
    public class AndroidCanvasRenderer : SKCanvasViewRenderer
    {
        private AndroidGestureCatcher catcher;
        public AndroidCanvasRenderer()
        {
            catcher = new AndroidGestureCatcher();
        }
        public override bool OnTouchEvent(MotionEvent e)
        {
            catcher.OnMotion(e);
            
            return true;
            //return base.OnTouchEvent(e);
        }
        protected override void OnElementChanged(ElementChangedEventArgs<SkiaSharp.Views.Forms.SKCanvasView> e)
        {
            base.OnElementChanged(e);
            foreach (var a in e.NewElement.GestureRecognizers)
            {
                if (a.GetType() == typeof(SingleTouchGestureRecognizer))
                {
                    (a as SingleTouchGestureRecognizer).SetCatcher(catcher);
                }
                else if (a.GetType() == typeof(DoubleTouchGestureRecognizer))
                {
                    (a as DoubleTouchGestureRecognizer).SetCatcher(catcher);
                }
            }

            //Control.GenericMotion += catcher.OnMotion;
            //Control.Touch += HandleTouch;
           
        }

        private bool TestM(MotionEvent arg)
        {
            throw new NotImplementedException();
        }
    }
}