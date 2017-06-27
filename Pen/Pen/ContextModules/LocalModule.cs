using Ninject;
using Ninject.Modules;
using Pen.Layering;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.ContextModules
{
    public class LocalModule : NinjectModule
    {
        private ContextManager _manager;
        public LocalModule(ContextManager manager)
        {
            _manager = manager;
        }
        public override void Load()
        {
            Bind<ContextManager>().ToConstant(_manager).InSingletonScope();
            Bind<PSize>().ToConstant(_manager.GlobalKernel.Get<PSize>()).InSingletonScope();
            Bind<CanvasPage>().ToSelf().InSingletonScope();


            Bind<BitmapWrapper>().ToSelf().InTransientScope();
            Bind<CanvasWrapper>().ToSelf().InTransientScope();
           
            
        }
    }
}
