using Pen.ContextModules;
using Pen.Gestures;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Pen.Layering;
using SkiaSharp;
using Pen.LibraryExtensions;
using Pen.Drawing.Services;
using Pen.MathExtenions;
using Pen.Geometry;

namespace Pen.UI.MainCanvas
{
    public abstract class TouchCanvas : SKCanvasView
    {
       protected PVector Center { get { return new PVector(X+Bounds.Width/2, Y+Bounds.Height/2); } }
        public TouchCanvas(DoubleTouchGestureRecognizer dt, SingleTouchGestureRecognizer st)
        {
          
            dt.MoveTransform += PanCanvas;
            dt.ScaleTransform += ScaleCanvas;
            dt.RotateTransform += RotateCanvas;
            st.StartedTouch += StartTouch;
            st.MovedTouch += MoveTouch; 
            st.ExitedTouch += ExitTouch;
            st.CancelledTouch += CancelTouch;
            st.EndedTouch += EndTouch;
            GestureRecognizers.Add(st);
            GestureRecognizers.Add(dt);
            PaintSurface += DrawCanvas;
           
        }

        protected virtual void Redraw()
        {
            InvalidateSurface();
        }

        protected abstract void DrawCanvas(object sender, SKPaintSurfaceEventArgs e);

        protected abstract void EndTouch(PTouch args);

        protected abstract void CancelTouch(PTouch args);

        protected abstract void ExitTouch(PTouch args);

        protected abstract void MoveTouch(PTouch args);

        protected abstract void RotateCanvas(RotateEventArgs args);

        protected abstract void StartTouch(PTouch args);

        protected abstract void ScaleCanvas(ScaleEventArgs args);

        protected abstract void PanCanvas(MoveEventArgs args);


    }
}
