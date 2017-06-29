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
using Pen.UI.MainCanvas;

namespace Pen.Drawing.Services
{
    public class CentralDrawingService
    {
        private IShape Shape;
        public delegate void DrawingChanged();
        public event DrawingChanged OnChange;
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
        public void InitializeDrawing(PTouch touch)
        {
            _layerManager.SetDrawingAsTemporary();
            GetFromKernel();
            Shape.Initialize(touch);
            _layerManager.ClearTemporary();
            DrawStep();

        }
        public void UpdateShape(PTouch touch)
        {
            Shape.Update(touch);
            _layerManager.ClearTemporary();
            DrawStep();
        }
        public void CancelDrawing(PTouch touch)
        {
            _layerManager.ClearTemporary();
            Clear();
            OnChange?.Invoke();
        }
        public void FinalizeDrawing(PTouch touch)
        {
            _layerManager.ClearTemporary();
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
            OnChange?.Invoke();
        }
    }
}
