using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry;
using Pen.Geometry.GeometryShapes;
using Pen.ContextModules;

namespace Pen.Drawing.Shapes
{
    public class LineShape : AbstractShape
    {
        public override IGeometricShape BaseShape { get; set; }
        
        public LineShape(ContextManager cm, LineGeometry ln):base(cm)
        {
            BaseShape = ln;
        }

        protected override List<PVector> GetDataToDraw()
        {
            return BaseShape.ControlPoints;
        }
    }
}
