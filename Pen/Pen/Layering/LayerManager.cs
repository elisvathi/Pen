using Pen.ContextModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Pen.Layering
{
    public class LayerManager
    {
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
        }
        public PLayer ActiveLayer { get { if (ActiveLayerIndex >= 0) return _layers[ActiveLayerIndex]; else return null; } }
        public void AddNewLayer()
        {
            _layers.Add(_manager.ActiveKernel.Get<PLayer>());
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
            }
        }
        public void RemoveLayer(int i)
        {
            RemoveLayer(_layers[i]);
        }
        private void ChangeActiveLayerAutomatically()
        {
            if (_layers.Count > 0) { ActiveLayerIndex = _layers.Count + 1; } else { ActiveLayerIndex = 1; }
        }

    }
}
