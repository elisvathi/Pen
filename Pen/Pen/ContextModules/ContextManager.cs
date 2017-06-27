using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.ContextModules
{
    public class ContextManager
    {
        List<IKernel> _localKernels;
        IKernel _globalKernel;
        
        public ContextManager([Named("GlobalKernel")]IKernel global)
        {
            GlobalKernel = global;
            _localKernels = new List<IKernel>();
            AddNewKernel();
        }
        private int _ActiveKernelIndex = -1;
        public IKernel GlobalKernel { get => _globalKernel; set => _globalKernel = value; }
        public List<IKernel> LocalKernels { get => _localKernels; set => _localKernels = value; }
        public IKernel ActiveKernel { get { if (_ActiveKernelIndex >= 0) { return LocalKernels[_ActiveKernelIndex]; } else { return null; } } }
        public void AddNewKernel(IKernel kernel)
        {
            _localKernels.Add(kernel);
            ChangeActiveKernelAutomaitcally();
        }
        private void ChangeActiveKernelAutomaitcally()
        {
            _ActiveKernelIndex = LocalKernels.Count - 1;
        }
        public void AddNewKernel()
        {
            var kern = new StandardKernel(new LocalModule(this));
            AddNewKernel(kern);
        }
        public void RemoveKernel(int index)
        {
            LocalKernels.RemoveAt(index);
            ChangeActiveKernelAutomaitcally();

        }
        public void RemoveKernel(IKernel kernel)
        {
            if (LocalKernels.Contains(kernel)) { LocalKernels.Remove(kernel); }
            ChangeActiveKernelAutomaitcally();
        }

    }
}
