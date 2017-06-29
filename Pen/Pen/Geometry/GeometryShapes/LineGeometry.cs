using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Geometry.GeometryShapes
{
    public class LineGeometry : IGeometricShape
    {
        private PLine baseGeometry;
        public LineGeometry()
        {
            baseGeometry = new PLine();
        }

        public List<PVector> ControlPoints
        {
            get { return new List<PVector>() { baseGeometry.Start.Copy(), baseGeometry.End.Copy() }; }
        }
        public void AddStartPoint(PVector p)
        {
            baseGeometry.Start = p;
            baseGeometry.End = p;
        }

        public void AddUpdatePoint(PVector p)
        {
            baseGeometry.End = p;
        }

        public void FinalPoint(PVector p)
        {
            baseGeometry.End = p;
        }

        public List<PVector> GetDividePoints(int n)
        {
            return baseGeometry.Divide(n).ToList();
        }

        public void UpdateWithControlPoints(List<PVector> data)
        {
            baseGeometry.Start = data[0];
            baseGeometry.End = data[1];
        }

    }
}
