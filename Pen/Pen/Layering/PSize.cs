using Pen.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Layering
{
    public struct PSize
    {
        public int Width;
        public int Height;
        public PVector Center { get { return new PVector(Width / 2, Height / 2); } }
    }
}
