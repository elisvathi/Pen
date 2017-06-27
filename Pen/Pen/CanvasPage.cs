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
        public delegate void DrawOnCanvasDelegate(SKPaintSurfaceEventArgs args);
        public event DrawOnCanvasDelegate CanvasWillRedraw;
        bool shoFill = true;
        bool isEnabled = false;
        double radius = 100;
        private SKCanvasView _CanvasView;
        public CanvasPage()
        {
            Title = "SIMPLE CIRCLE";
            _CanvasView = new SKCanvasView();
            _CanvasView.PaintSurface += OnPaintSurface;
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += TappedEvent;
            _CanvasView.GestureRecognizers.Add(tapGesture);
            //Content = _CanvasView;
            var but = new Button();
            but.Clicked += OnButtonClicked;
            but.Text = "CLICK ME";
            var layout = new StackLayout();
            layout.Children.Add(but);
            layout.Children.Add(_CanvasView);
            Content = layout;
            
            _CanvasView.PaintSurface += OnCanvasWillRedraw;
            _CanvasView.HorizontalOptions = LayoutOptions.FillAndExpand;
            _CanvasView.VerticalOptions = LayoutOptions.FillAndExpand;
            
        }

        private void DrawLine(SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 25
            };
            canvas.DrawLine(0, 0, info.Width, info.Height,paint);
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            if (!isEnabled) { CanvasWillRedraw += DrawLine; isEnabled = true; } else
            {
                CanvasWillRedraw -= DrawLine; isEnabled = false;
            }
            Redraw();
        }

        private void TappedEvent(object sender, EventArgs e)
        {
            shoFill ^= true;
            Redraw();
        }
        protected virtual void OnCanvasWillRedraw(object Sender, SKPaintSurfaceEventArgs args)
        {
            CanvasWillRedraw?.Invoke(args);
        }
        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            radius += 20;
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            //canvas.Clear();
            var bit = new SKBitmap(5000, 5000);
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 25
            };
            canvas.DrawCircle(info.Width / 2, info.Height / 2, (float)radius, paint);
            if (shoFill)
            {
                paint.Style = SKPaintStyle.Fill;
                paint.Color = SKColors.Blue;
                canvas.DrawCircle(args.Info.Width / 2, args.Info.Height / 2, 100, paint);
            }
        }
        public void Redraw()
        {
            _CanvasView.InvalidateSurface();
        }
    }
}