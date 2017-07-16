using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Geometry
{
  public static class PCurve
    {
        
        private static PVector GetLineParam(PLine l, float t)
        {
            return l.GetPointFromPosition(t);
        }
        private static List<PVector> GetNewPolygon(List<PVector> pts ,float t)
        {
            var retVal = new List<PVector>();
            for(int i = 0;i< pts.Count - 1; i++)
            {
                var l = new PLine(pts[i], pts[i + 1]);
                retVal.Add(GetLineParam(l, t));
            }
            return retVal;
        }
        private static PVector GetFinalPoint(List<PVector> vec,float t)
        {
            var g = vec;
            while(g.Count > 1)
            {
                g = GetNewPolygon(g,t);
            }
            return g[0];

        }
        public static List<PVector> GetCurveData(List<PVector> points, int steps)
        {
            var retVal = new List<PVector>
            {
                points[0]
            };
            float stepSize = (float)1 / (float)steps;
            for(float i = stepSize; i< 1; i++) {
                retVal.Add(GetFinalPoint(points, i));
            }
            retVal.Add(points.Last());
            return retVal;
        }
    }
}
