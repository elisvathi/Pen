using Pen.Geometry;
using Pen.MathExtenions;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.LibraryExtensions
{
   public static class SkiaExtensions
    {
        public static SKRect AlignRects( this SKRectI parent, SKRect child)
        {
            
            var child2 = new SKRect(0, 0, child.Width, child.Height);
            var widthDifference = parent.Width - child2.Width;
            var heightDifference = parent.Height - child2.Width;
            widthDifference /= 2;
            heightDifference /= 2;
            return  new SKRect(parent.Left + widthDifference, parent.Top + heightDifference, parent.Right - widthDifference, parent.Bottom - heightDifference);
        }
        public static SKPoint ToSKPoint(this PVector vec)
        {
            return new SKPoint(vec.X.ToFloat(), vec.Y.ToFloat());
        }
        public static void EmptyBitmap(this SKBitmap bmp)
        {
            var pn = new SKPaint();
            pn.BlendMode = SKBlendMode.Dst;
            bmp.Erase(SKColors.Beige);
        }
    }
}
