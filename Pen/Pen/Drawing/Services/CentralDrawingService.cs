using Pen.ContextModules;
using Pen.Drawing.Brushes;
using Pen.Drawing.Rulers;
using Pen.Drawing.Shapes;
using Pen.Layering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Pen.Gestures;

namespace Pen.Drawing.Services
{
    public class CentralDrawingService
    {
        private IShape Shape;
        private LayerManager _layerManager;
        private ContextManager _manager;
        public CentralDrawingService(LayerManager lm, ContextManager manager)
        {
            _layerManager = lm;
            _manager = manager;

        }
        private bool IsInitialized()
        {
            return Shape != null;
        }
        private void InitializeDrawing(PTouch touch)
        {
            _layerManager.SetDrawingAsTemporary();
            GetFromKernel();
            Shape.Initialize(touch);
            DrawStep();
        }
        private void UpdateShape(PTouch touch)
        {
            Shape.Update(touch);
            DrawStep();
        }
        private void FinalizeDrawing(PTouch touch)
        {
            _layerManager.SetDrawingAsFinal();
            Shape.FinalizeShape(touch);
            DrawStep();
            Clear();
        }

        private void Clear()
        {
            Shape = null;

        }
        private void GetFromKernel()
        {
            Shape = _manager.ActiveKernel.Get<IShape>();

        }

        private void DrawStep()
        {
            Shape.DrawOnScreen();
        }
    }
}
