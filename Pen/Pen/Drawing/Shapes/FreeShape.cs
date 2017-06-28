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
   public class FreeShape : AbstractShape
    {
        public override IGeometricShape BaseShape { get; set; }
        public FreeShape(ContextManager manager, PointCollectionGeometry geom):base(manager)
        {
            BaseShape = geom;
        }

       

        protected override List<PVector> GetDataToDraw()
        {
            return new List<PVector>(BaseShape.ControlPoints);
        }
    }
}
