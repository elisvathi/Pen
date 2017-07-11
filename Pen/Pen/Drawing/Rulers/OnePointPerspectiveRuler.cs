using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry;

namespace Pen.Drawing.Rulers
{
    public class OnePointPerspectiveRuler : PerspectiveRuler
    {
        public override List<PVector> ControlPoints { get { return new List<PVector>() { base.ControlPoints[0] }; } set { base.ControlPoints[0] = value[0]; } }
    }
}
