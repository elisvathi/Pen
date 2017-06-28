using Pen.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Drawing.Rulers
{
    public interface IRuler
    {
        List<PVector> ControlPoints { get; set; }
        double RulerAffectWeight { get; set; }
        List<PVector> ApplyRuler(List<PVector> inputData);
    }
}
