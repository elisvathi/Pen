using Ninject;
using Ninject.Modules;
using Pen.Layering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.ContextModules
{
    public class GlobalModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IKernel>().ToConstant(this.Kernel).InSingletonScope().Named("GlobalKernel");
            Bind<ContextManager>().ToSelf().InSingletonScope();
            Bind<App>().ToSelf().InSingletonScope();

            var sz = new PSize() {Width = 1080, Height = 1920 };
            Bind<PSize>().ToConstant(sz).InTransientScope();
        }
    }
}
