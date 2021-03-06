﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry;

namespace Pen.Drawing.Rulers
{
    public class TwoPointPerspectiveRuler : PerspectiveRuler

    {
        public override List<PVector> ControlPoints
        {
            get
            {
                return new List<PVector>() { base.ControlPoints[0], base.ControlPoints[1] };
            }
            set
            {
                base.ControlPoints[0] = value[0];
                base.ControlPoints[1] = value[1];
            }
        }
    }
}
