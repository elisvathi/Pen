using Pen.ContextModules;
using Pen.Drawing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Ninject;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Pen.UI.CustomControls;
using Pen.Geometry;
using Pen.UI.MainCanvas;
using Pen.UI.Helpers;

namespace Pen.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class ColorPickerView : ContentView
    {
        [Inject]
        public BackgroundImage BgImg { get; set; }

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
            HueJitterSlider.BindingContext = Service;
            SaturationJitterSlider.BindingContext = Service;
            LightnessJitterSlider.BindingContext = Service;
            HardnessJitter.BindingContext = Service;
            BMSelector.ItemsSource = PBlendingMode.Names;
            

            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream strem = assembly.GetManifestResourceStream("Pen.bg.bmp"))
            using (SKManagedStream skStream = new SKManagedStream(strem))
            using (SKBitmap bitma =  SKBitmap.Decode(skStream))
                using(SKShader shader = SKShader.CreateBitmap(bitma, SKShaderTileMode.Repeat, SKShaderTileMode.Repeat))
            {
                //BgPaint.Shader = shader;
            };
            var a = manager.ActiveKernel.Get<BackgroundImage>();
            BgPaint.Shader = a.GetShader();

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
        SKPaint BgPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill
        };

        private void SKCanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canv = e.Surface.Canvas;
            var info = e.Info;
            canv.DrawPaint(BgPaint);
            
            //canv.DrawCircle(info.Width / 2, info.Height / 2, Math.Min(info.Height, info.Width) * 0.8F, Service.GetBrush(new PVector(info.Width / 2, info.Height / 2)));
            var p = new List<PVector>
            {
                new PVector(info.Width * 0.2, info.Height * 0.2),
                new PVector(info.Width *0.33 , info.Height*1),
                new PVector(info.Width / 3 * 2, info.Height*0.2),
                new PVector(info.Width * 0.8, info.Height*0.8)
            };
            var cdata = new PCurve(p, Geometry.GeometryShapes.CurveType.Normal);
            _renderer.DrawCurve(cdata, canv);
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            SkiaSurf.InvalidateSurface();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(manager.ActiveKernel.Get<MainCanvasPage>());
        }

        private void BMSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var a = BMSelector.SelectedItem as String;
            Service.BMode.ModeName = a;
            SkiaSurf.InvalidateSurface();
        }
    }
}