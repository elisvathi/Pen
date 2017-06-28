using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry.GeometryShapes;
using Pen.Gestures;
using Pen.Drawing.Brushes;
using Pen.Drawing.Rulers;
using Pen.ContextModules;
using Ninject;
using Pen.Geometry;

namespace Pen.Drawing.Shapes
{
    public  abstract class AbstractShape : IShape
    {
        private ContextManager _manager;
        private IRuler Ruler { get { return _manager.ActiveKernel.Get<IRuler>(); } }
        private IBrush Brush { get { return _manager.ActiveKernel.Get<IBrush>(); } }
        public AbstractShape(ContextManager manager)
        {
            Touches = new List<PTouch>();
            _manager = manager;
        }
        public List<PTouch> Touches { get; set; }
        public abstract IGeometricShape BaseShape { get; set; }

        public  void DrawOnScreen()
        {
            Brush.Draw(GetDataToDraw());
        }
        

        public virtual void FinalizeShape(PTouch finalPoint)
        {
            BaseShape.FinalPoint(finalPoint.Position);
        }
        protected abstract List<PVector> GetDataToDraw();
        public virtual void Initialize(PTouch initPoint)
        {
            BaseShape.AddStartPoint(initPoint.Position);
            Touches.Add(initPoint);
        }

        public void Update(PTouch updatePoint)
        {
            BaseShape.AddUpdatePoint(updatePoint.Position);
            Touches.Add(updatePoint);
        }

        public void UpdateWithRuler()
        {
            var ruledData = Ruler.ApplyRuler(BaseShape.ControlPoints);
            BaseShape.UpdateWithControlPoints(ruledData);
        }
    }
}
