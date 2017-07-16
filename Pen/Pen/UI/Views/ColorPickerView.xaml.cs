using Pen.ContextModules;
using Pen.Drawing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Pen.UI.CustomControls;
using Pen.Geometry;

namespace Pen.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorPickerView : ContentView
    {
        private bool StrokeFillSwitch = true;
        private ContextManager manager;
        private BindableColor Scolor { get { return manager.ActiveKernel.Get<BindableColor>("StrokeColor"); } }
        private BindableColor Fcolor { get { return manager.ActiveKernel.Get<BindableColor>("FillColor"); } }
        private SKPaint StrokePaint { get { return manager.ActiveKernel.Get<DrawingConfigService>().SPaint; } }
        private SKPaint FillPaint { get { return manager.ActiveKernel.Get<DrawingConfigService>().FPaint; } }
        private DrawingConfigService Service { get { return manager.ActiveKernel.Get<DrawingConfigService>(); } }
        private PRenderer _renderer { get { return manager.ActiveKernel.Get<PRenderer>(); } }
        public ColorPickerView(ContextManager mg)
        {
            manager = mg;
            InitializeComponent();
            SetBindingContext();
            StrokeWidthSlider.BindingContext = Service;
            HardnesSlider.BindingContext = Service;
           
            SpacingSlider.BindingContext = Service;
            XDispSlider.BindingContext = Service;
            YDispSlider.BindingContext = Service;
            OpacityRandomSlider.BindingContext = Service;
            ScaleSlider.BindingContext = Service;
        }

        private void SetBindingContext()
        {
            BindingContext = StrokeFillSwitch ? Scolor : Fcolor;
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            StrokeFillSwitch = !StrokeFillSwitch;
            SetBindingContext();
        }

        private void SKCanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canv = e.Surface.Canvas;
            var info = e.Info;
            canv.Clear();
            //canv.DrawCircle(info.Width / 2, info.Height / 2, Math.Min(info.Height, info.Width) * 0.8F, Service.GetBrush(new PVector(info.Width/2, info.Height/2)));
            var p = new List<PVector>
            {
                new PVector(0, 0),
                new PVector(info.Width / 4, info.Height*0.8),
                new PVector(info.Width / 4 * 3, info.Height * 0.2),
                new PVector(info.Width, info.Height*0.8)
            };
           var cdata =  PCurve.GetCurveData(p, 15);
            _renderer.DrawPath(cdata, canv);
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            SkiaSurf.InvalidateSurface();
        }
    }
}