using Ninject;
using Ninject.Modules;
using Pen.Drawing.Brushes;
using Pen.Drawing.Rulers;
using Pen.Drawing.Services;
using Pen.Drawing.Shapes;
using Pen.Geometry.GeometryShapes;
using Pen.Gestures;
using Pen.Layering;
using Pen.UI.MainCanvas;
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
            Bind<MainTouchCanvas>().ToSelf().InSingletonScope();

            Bind<BitmapWrapper>().ToSelf().InTransientScope();
            Bind<CanvasWrapper>().ToSelf().InTransientScope();

            Bind<IRuler>().To<NoRuler>().InSingletonScope();
            Bind<IBrush>().To<SimpleBrush>().InSingletonScope();
            Bind<IShape>().To<FreeShape>().InTransientScope();

            Bind<DrawingConfigService>().ToSelf().InSingletonScope();
            Bind<CentralDrawingService>().ToSelf().InSingletonScope();
            Bind<PRenderer>().ToSelf().InSingletonScope();

            Bind<LineGeometry>().ToSelf().InTransientScope();
            Bind<PointCollectionGeometry>().ToSelf().InTransientScope();

            Bind<LayerManager>().ToSelf().InSingletonScope();
            Bind<PLayer>().ToSelf().InTransientScope();

            Bind<DoubleTouchGestureRecognizer>().ToSelf().InTransientScope();
            Bind<SingleTouchGestureRecognizer>().ToSelf().InTransientScope();
        }
    }
}
