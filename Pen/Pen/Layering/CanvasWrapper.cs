using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Layering
{
   public class CanvasWrapper
        
    {
        private SKCanvas _canvas;
        public CanvasWrapper(BitmapWrapper bitmap)
        {
            _canvas = new SKCanvas(bitmap.SBitmap);
            
        }
        public SKCanvas SCanvas { get=>_canvas; }
    }
}
