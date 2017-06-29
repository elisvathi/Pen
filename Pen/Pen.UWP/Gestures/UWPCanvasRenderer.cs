using Pen.Gestures;
using Pen.UWP.Gestures;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Platform.UWP;
using System;
using Windows.UI.Xaml.Input;
using Pen.UI.MainCanvas;

[assembly: ExportRenderer(typeof(TouchCanvas), typeof(UWPCanvasRenderer))]
namespace Pen.UWP.Gestures
{
    public class UWPCanvasRenderer : SKCanvasViewRenderer
    {
        private UWPGestureHandler catcher;
        public UWPCanvasRenderer() : base()
        {
            //TODO: Change with platform specific Class
            catcher = new UWPGestureHandler();
            System.Diagnostics.Debug.Write("LOADED RENDERER");
        }



        protected override void OnElementChanged(ElementChangedEventArgs<SKCanvasView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                if (!e.NewElement.GestureRecognizers.Any())
                    return;
                
                foreach(var a in e.NewElement.GestureRecognizers)
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

                
                Control.PointerPressed += catcher.PointerPressed;
                Control.PointerMoved += catcher.PointerMoved;
                Control.PointerReleased += catcher.PointerReleased;
                Control.PointerCanceled += catcher.PointerCancelled;
                Control.PointerExited += catcher.PointerExited;
            }
        }

       
    }
}
