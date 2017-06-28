using Pen.Geometry;
using Pen.Layering;
using Pen.MathExtenions;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Drawing.Services
{
    public class PRenderer
    {
        private LayerManager _layerManager;
        private DrawingConfigService _BrushInfoService;
        public PRenderer(LayerManager lm, DrawingConfigService serv) { _layerManager = lm; _BrushInfoService = serv; }
        private SKCanvas ActiveCanvas { get { return _layerManager.CanvasToDraw; } }
        public void DrawLine(PVector start, PVector end)
        {
            DrawLine(start, end, _BrushInfoService.SPaint);
        }
        public void DrawLine(PVector start, PVector end, SKPaint p)
        {
            ActiveCanvas.DrawLine(start.X.ToFloat(), start.Y.ToFloat(), end.X.ToFloat(), end.Y.ToFloat(), p);
        }
        public void DrawCircle(PVector center, double radius)
        {
            DrawCircle(center, radius, _BrushInfoService.SPaint);
        }
        public void DrawCircle(PVector center, double radius, SKPaint paint)
        {
            ActiveCanvas.DrawCircle(center.X.ToFloat(), center.Y.ToFloat(), radius.ToFloat(), paint);
        }
    }
}
