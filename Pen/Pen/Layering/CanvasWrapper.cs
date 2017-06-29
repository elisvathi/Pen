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
        private BitmapWrapper _bmp;
        public CanvasWrapper(BitmapWrapper bitmap)
        {
            _bmp = bitmap;
            _canvas = new SKCanvas(_bmp.SBitmap);
            
        }
        public SKCanvas SCanvas { get=>_canvas; }
        public SKBitmap GetBitmap { get => _bmp.SBitmap; }
    }
}
