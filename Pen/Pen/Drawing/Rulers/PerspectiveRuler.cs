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
        private double[] MinimumPositiveAnglesWithVanishingPoints(PVector start, PVector end)
        {
            var retVal = new List<double>();
            var vec1 = PVector.Sub(end, start);
            var vec2 = PVector.Sub(start, end);
            foreach (var v in VanishingPoints)
            {
                var v1 = PVector.Sub(start, v);
                var v2 = PVector.Sub(end, v);
                var ang1 = PVector.AngleBetweenDegrees(v1, vec1);
                var ang2 = PVector.AngleBetweenDegrees(v2, vec2);
                ang1 = FixAngle(ang1);
                ang2 = FixAngle(ang2);
                retVal.Add(Math.Min(ang1, ang2));
            }
            return retVal.ToArray();

        }
        private double FixAngle(double ang1)
        {
            var ang2 = Math.Abs(ang1);
            ang2 = ang2 % 360;
            var compAng1 = 360 - ang1;
            ang2 = Math.Min(compAng1, ang2);
            return ang2;
        }
        private int MinIndex(double[] values)
        {
            int retVal = -1;
            double refe = Double.PositiveInfinity;
            for(int i = 0;i< values.Length; i++) {
                if(values[i] < refe)
                {
                    refe = values[i];
                    retVal = i;
                }
            }
            return retVal;
        }
        private int VpIndex(PVector start, PVector end)
        {
            return MinIndex(MinimumPositiveAnglesWithVanishingPoints(start, end));
        }
        private PVector RuledPoint(PVector start, PVector end)
        {
            var l = new PLine(start, end);
            l.HeadingTo(VanishingPoints[VpIndex(start, end)], start, end);
            return l.End;

        }
        protected override List<PVector> CalculateData(List<PVector> inputData)
        {
            var retVal = new List<PVector>();
            retVal.Add(inputData[0]);
            for (var i = 0;i< inputData.Count- 1; i++)
            {
                var p1 = inputData[i];
                var p2 = inputData[i + 1];
                retVal.Add(RuledPoint(p1, p2));
            }
            return retVal;
        }
    }
}
