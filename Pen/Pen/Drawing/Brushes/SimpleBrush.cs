using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry;
using Pen.Drawing.Services;
using SkiaSharp;
using Pen.LibraryExtensions;

namespace Pen.Drawing.Brushes
{
    public class SimpleBrush : AbstractBrush
    {
        public SimpleBrush(PRenderer ren) : base(ren)
        {
        }

        public override void Draw(List<PVector> data)
        {
            for (int i = 0; i < data.Count - 1; i++)
            {
                _renderer.DrawLine(data[i], data[i + 1]);
            }
            var path = new SKPath();
            var converted = new List<SKPoint>();
            foreach(var p in data)
            {
                converted.Add(p.ToSKPoint());
            }
            path.AddPoly(converted.ToArray());
            _renderer.DrawPath(path);
        }
    }
}
