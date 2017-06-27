using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Layering
{
    public class BitmapWrapper
    {
        private SKBitmap bitmap;
        public BitmapWrapper( PSize sz)
        {
            bitmap = new SKBitmap(sz.Width, sz.Height);
        }

        public SKBitmap SBitmap { get => bitmap; }
    }
}
