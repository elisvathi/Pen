using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Pen
{
    public class CanvasPage : ContentPage
    {
        bool shoFill = true;
        public CanvasPage()
        {
            Title = "SIMPLE CIRCLE";
            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnPaintSurface;
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += TappedEvent;
            canvasView.GestureRecognizers.Add(tapGesture);
            Content = canvasView;
        }

        private void TappedEvent(object sender, EventArgs e)
        {
            shoFill ^= true;
            (sender as SKCanvasView).InvalidateSurface();
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 25
            };
            canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);
            if (shoFill) {
            paint.Style = SKPaintStyle.Fill;
            paint.Color = SKColors.Blue;
            canvas.DrawCircle(args.Info.Width / 2, args.Info.Height / 2, 100, paint);
            }
        }
    }
}