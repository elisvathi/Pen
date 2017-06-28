using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Geometry.GeometryShapes
{
   public interface IGeometricShape
    {
        void AddStartPoint(PVector p);
        void AddUpdatePoint(PVector p);
        void FinalPoint(PVector p);
        void UpdateWithControlPoints(List<PVector> data);
        List<PVector> ControlPoints { get; }
        List<PVector> GetDividePoints(int n);
    }
}
