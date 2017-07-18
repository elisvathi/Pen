using Pen.ContextModules;
using Pen.Layering;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Xamarin.Forms;
using Pen.UI.Helpers;
using SkiaSharp;

namespace Pen.UI.Views.LayerView
{
    public class LayerThumbnail : ContentView
    {
        private PLayer layer;
        private ContextManager manager;
        public LayerThumbnail(PLayer l, ContextManager mg)
        {
            manager = mg;
            layer = l;
            var canv = new SKCanvasView();
            canv.PaintSurface += PaintLayer;
            Content = new StackLayout
            {
                Children = {
                   canv
                }
            };
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            //HeightRequest =  Width*(l.GetBitmap.Width/l.GetBitmap.Height);
            BgPaint.Shader = manager.ActiveKernel.Get<BackgroundImage>().GetShader();
        }
        private SKPaint BgPaint = new SKPaint()
        {
           
            Style = SKPaintStyle.Fill
        };

        private void PaintLayer(object sender, SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canv = e.Surface.Canvas;
            canv.DrawPaint(BgPaint);
            var bmp = layer.GetBitmap;
            canv.DrawBitmap(layer.GetBitmap, new SkiaSharp.SKRect(0, 0, info.Width, info.Height));

        }
    }
}