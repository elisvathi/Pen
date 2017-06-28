using Pen.Gestures;
using Pen.UWP.Gestures;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(SkiaSharp.Views.UWP.SKXamlCanvas), typeof(CustcomCanvasRenderer))]
namespace Pen.UWP.Gestures
{
    public class CustcomCanvasRenderer : SKCanvasViewRenderer
    {
        private UWPGestureHandler catcher;
        public CustcomCanvasRenderer() : base()
        {
            //TODO: Change with platform specific Class
            catcher = new UWPGestureHandler();

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
