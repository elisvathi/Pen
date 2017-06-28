using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SkiaSharp.Views.Forms;
using SkiaSharp.Views.Android;
using Xamarin.Forms.Platform.Android;
using Pen.Gestures;
using Xamarin.Forms;
using Pen.Droid.Gestures;

[assembly: ExportRenderer(typeof(SkiaSharp.Views.Android.SKCanvasView), typeof(CustomCanvasRenderer))]
namespace Pen.Droid.Gestures
{
    public class CustomCanvasRenderer : SKCanvasViewRenderer
    {
        private AndroidGestureCatcher catcher;
        public CustomCanvasRenderer()
        {
            catcher = new AndroidGestureCatcher();
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

            Control.GenericMotion += catcher.OnMotion;
        }


    }
}