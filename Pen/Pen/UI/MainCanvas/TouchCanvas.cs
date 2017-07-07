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

namespace Pen.UI.MainCanvas
{
    public class TouchCanvas : SKCanvasView
    {
        ContextManager _manager;
        CentralDrawingService _service;
        public TouchCanvas(ContextManager cm, DoubleTouchGestureRecognizer dt, SingleTouchGestureRecognizer st, CentralDrawingService serv)
        {
            _service = serv;
            _manager = cm;
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
            PaintSurface += DrawLayers;
            _service.OnChange += Redraw;
        }

        private void Redraw()
        {
           InvalidateSurface();
        }

        private LayerManager Layer_Manager { get { return _manager.ActiveKernel.Get<LayerManager>(); } }
        private void DrawLayers(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canvas = e.Surface.Canvas;
            canvas.Clear();
            foreach (var l in Layer_Manager.Layers)
            {
                var bmp = l.GetBitmap;
                var rect = new SKRect(0, 0, bmp.Width, bmp.Height);

                canvas.DrawBitmap(l.GetBitmap, info.Rect);
            }
            var temp = Layer_Manager.TempLayer.GetBitmap;
            var temprect = new SKRect(0, 0, temp.Width, temp.Height);
            canvas.DrawBitmap(temp, info.Rect);
        }

        private void EndTouch(PTouch args)
        {
            _service.FinalizeDrawing(args);
        }

        private void CancelTouch(PTouch args)
        {
            _service.CancelDrawing(args);

        }

        private void ExitTouch(PTouch args)
        {
            _service.CancelDrawing(args);
        }

        private void MoveTouch(PTouch args)
        {
            _service.UpdateShape(args);
        }

        private void StartTouch(PTouch args)
        {
            _service.InitializeDrawing(args);
        }

        private void RotateCanvas(RotateEventArgs args)
        {
            this.Rotation += args.Angle;
            //args.Center.DebugVector();
        }

        private void ScaleCanvas(ScaleEventArgs args)
        {
            this.Scale *= args.Value;
        }

        private void PanCanvas(MoveEventArgs args)
        {
            //this.TranslationX += args.Displacement.X;
            //this.TranslationY += args.Displacement.Y;
        }
    }
}
