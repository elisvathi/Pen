using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Layering
{
   public class PLayer
    {
        private CanvasWrapper _canvas;
        public PLayer(CanvasWrapper canvas)
        {
            _canvas = canvas;
        }
        public CanvasWrapper Canvas { get => _canvas; }
    }
}
