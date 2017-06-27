using Pen.Geometry;
using Pen.MathExtenions;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Drawing.Services
{
   public class Renderer
    {
        public void DrawLine(SKCanvas canvas, PVector start, PVector end)
        {
            canvas.DrawLine(start.X.ToFloat(), start.Y.ToFloat(), end.X.ToFloat(), end.Y.ToFloat(), new SKPaint());
        }
    }
}
