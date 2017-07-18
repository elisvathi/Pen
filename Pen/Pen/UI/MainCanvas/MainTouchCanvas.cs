using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.ContextModules;
using Pen.Drawing.Services;
using Pen.Gestures;
using Pen.Layering;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Ninject;

namespace Pen.UI.MainCanvas
{
    public class MainTouchCanvas : TouchCanvas
    {
        protected ContextManager _manager;
        protected CentralDrawingService _service;
        public MainTouchCanvas(ContextManager cm, DoubleTouchGestureRecognizer dt, SingleTouchGestureRecognizer st, CentralDrawingService serv) : base(dt, st)
        {
            _manager = cm;
            _service = serv;
            _service.OnChange += Redraw;
        }


        private LayerManager Layer_Manager { get { return _manager.ActiveKernel.Get<LayerManager>(); } }
        protected override void DrawCanvas(object sender, SKPaintSurfaceEventArgs e)
        {

            var info = e.Info;
            var canvas = e.Surface.Canvas;
            canvas.Clear();
            foreach (var l in Layer_Manager.Layers)

            {
                var bmp = l.GetBitmap;

                //l.Canvas.SCanvas.DrawBitmapNinePatch()
                var rect = new SKRect(0, 0, info.Width, info.Height);

                canvas.DrawBitmap(l.GetBitmap, new SKRect(0, 0, l.GetBitmap.Width, l.GetBitmap.Height));
                if (Layer_Manager.IsActive(l))
                {
                    var temp = Layer_Manager.TempLayer.GetBitmap;
                    var temprect = new SKRect(0, 0, temp.Width, temp.Height);
                    canvas.DrawBitmap(temp, temprect);
                }
            }

        }

        protected override void EndTouch(PTouch args)
        {
            _service.FinalizeDrawing(args);
        }

        protected override void CancelTouch(PTouch args)
        {
            _service.CancelDrawing(args);

        }

        protected override void ExitTouch(PTouch args)
        {
            _service.CancelDrawing(args);
        }

        protected override void MoveTouch(PTouch args)
        {
            _service.UpdateShape(args);
        }

        protected override void StartTouch(PTouch args)
        {
            _service.InitializeDrawing(args);
            System.Diagnostics.Debug.WriteLine("CALLED");
        }

        protected override void RotateCanvas(RotateEventArgs args)
        {
            this.Rotation += args.Angle;
            //args.Center.DebugVector();
            System.Diagnostics.Debug.WriteLine("ISROTATED");
        }

        protected override void ScaleCanvas(ScaleEventArgs args)
        {
            this.Scale *= args.Value;
        }

        protected override void PanCanvas(MoveEventArgs args)
        {
            //this.TranslationX += args.Displacement.X;
            //this.TranslationY += args.Displacement.Y;
        }
    }
}
