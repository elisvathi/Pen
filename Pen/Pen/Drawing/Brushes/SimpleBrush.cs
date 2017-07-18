using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry;
using Pen.Drawing.Services;
using SkiaSharp;
using Pen.LibraryExtensions;
using Pen.Geometry.GeometryShapes;

namespace Pen.Drawing.Brushes
{
    public class SimpleBrush : AbstractBrush
    {
        public SimpleBrush(PRenderer ren) : base(ren)
        {
        }
        private int lastAddedIndex = -1;
        public void FinalizeBrush() { lastAddedIndex = -1; }
        public void InitializeBrush() { lastAddedIndex = 0; }
        public override void Draw(List<PVector> data)
        {

            var crv = new PCurve(data, CurveType.Normal);
            _renderer.DrawCurve(crv);

            //if (data.Count > 1 && lastAddedIndex > -1)
            //{
            //    PVector p1, p2, p3;
            //    if (data.Count > lastAddedIndex + 2)
            //    {
                   

            //        if (lastAddedIndex == 0)
            //        {
            //            p1 = data[0];
            //        }
            //        else
            //        {
            //            var vec = PVector.Sub(data[lastAddedIndex + 1], data[lastAddedIndex]);
            //            vec.Mult(0.5);
            //            vec.Add(data[lastAddedIndex]);
            //            p1 = vec;
            //        }
            //        p2 = data[lastAddedIndex + 1];
            //        if (data.Count == lastAddedIndex + 3) { p3 = data[lastAddedIndex + 2]; }
            //        else
            //        {
            //            PVector vec = PVector.Sub(data[lastAddedIndex + 2], data[lastAddedIndex + 1]);
            //            vec.Mult(0.5); vec.Add(data[lastAddedIndex + 1]);
            //            p3 = vec;
            //        }
            //    }
            //    else
            //    {
            //        p1 = data[lastAddedIndex];
                    
            //        p3 = data[lastAddedIndex+1];
            //        var vec = PVector.Sub(p3, p1);
            //        vec.Mult(0.5);
            //        vec.Add(p1);
            //        p2 = vec;
            //    }
            //    var list = new List<PVector>() { p1, p2, p3 };
            //    var curve = new PCurve(list, CurveType.Loose);

            //}
        }
    }
}
