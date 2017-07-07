using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry;

namespace Pen.Drawing.Rulers
{
    public class PerspectiveRuler : AbstractRuler
    {
        private List<PVector> VanishingPoints;
        public override List<PVector> ControlPoints { get => VanishingPoints; set => VanishingPoints = value; }

        protected override List<PVector> CalculateData(List<PVector> inputData)
        {
            throw new NotImplementedException();
        }
    }
}
