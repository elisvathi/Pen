using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry;

namespace Pen.Drawing.Rulers
{
    public class NoRuler : AbstractRuler
    {
        public override List<PVector> ControlPoints { get; set; }

        public NoRuler()
        {
            ControlPoints = new List<PVector>();
        }
        protected override List<PVector> CalculateData(List<PVector> inputData)
        {
            return inputData;
        }
    }
}
