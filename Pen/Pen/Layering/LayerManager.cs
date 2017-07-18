using Pen.ContextModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using SkiaSharp;

namespace Pen.Layering
{
    public class LayerManager
    {
        private bool IsDrawingTemp;
        List<PLayer> _layers;
        PLayer _temp;
        private ContextManager _manager;
        private int ActiveLayerIndex = -1;

        public LayerManager(ContextManager manager)
        {
            _manager = manager;
            _temp = manager.ActiveKernel.Get<PLayer>();
            _layers = new List<PLayer>();
            AddNewLayer();
            IsDrawingTemp = true;
        }
        public bool IsActive(PLayer l)
        {
            if (Layers.Contains(l))
                if (Layers.IndexOf(l) == ActiveLayerIndex) return true;
            return false;

        }
        
        public SKCanvas CanvasToDraw { get { return IsDrawingTemp ? _temp.Canvas.SCanvas : ActiveLayer.Canvas.SCanvas; } }

        public PLayer ActiveLayer { get { if (ActiveLayerIndex >= 0) return _layers[ActiveLayerIndex]; else return null; } }
        public void AddNewLayer()
        {
            _layers.Add(_manager.ActiveKernel.Get<PLayer>());
            ChangeActiveLayerAutomatically();
        }
        public void RemoveLayer(PLayer l)
        {
            if (_layers.Contains(l))
            {
                if (_layers.Count == 1)
                {
                    AddNewLayer();
                    _layers.Remove(l);
                }
                ChangeActiveLayerAutomatically();
            }
        }
        public void RemoveLayer(int i)
        {
            RemoveLayer(_layers[i]);

        }
        public void MakeLayerActive(PLayer l)
        {
            if (Layers.Contains(l))
                ActiveLayerIndex = Layers.IndexOf(l);
        }
        private void ChangeActiveLayerAutomatically()
        {
            if (_layers.Count > 0) { ActiveLayerIndex = _layers.Count - 1; } else { ActiveLayerIndex = 1; }
        }

        public void SetDrawingAsTemporary() { IsDrawingTemp = true; }
        public void SetDrawingAsFinal() { IsDrawingTemp = false; }
        public void ClearTemporary() { _temp.ClearLayer(); }
        public List<PLayer> Layers { get { return _layers; } }
        public PLayer TempLayer { get { return _temp; } }
    }
}
