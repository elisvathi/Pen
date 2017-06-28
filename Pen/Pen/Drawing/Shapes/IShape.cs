using Pen.Geometry;
using Pen.Geometry.GeometryShapes;
using Pen.Gestures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Drawing.Shapes
{
    public interface IShape
    {
        List<PTouch> Touches { get; set; }
        IGeometricShape BaseShape { get; set; }
        void Initialize(PTouch initPoint);
        void Update(PTouch updatePoint);
        void FinalizeShape(PTouch finalPoint);
        void UpdateWithRuler();
        void DrawOnScreen();

    }
}
