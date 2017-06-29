using Pen.ContextModules;
using Pen.Gestures;
using Pen.UI.MainCanvas;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Xamarin.Forms;
using Pen.Layering;

namespace Pen
{
    public class CanvasPage : ContentPage
    {
        public delegate void DrawOnCanvasDelegate(SKPaintSurfaceEventArgs args);
        
        private TouchCanvas _CanvasView;
        private ContextManager Manager;
        public CanvasPage(TouchCanvas _canvas, ContextManager man)
        {
            Manager = man;
            Title = "SIMPLE CIRCLE";
            _CanvasView = _canvas;
           
           
            var but = new Button();
            but.Clicked += OnButtonClicked;
            but.Text = "CLICK ME";
            var layout = new StackLayout();
            layout.Children.Add(but);
            layout.Children.Add(_CanvasView);
            Content = layout;

            
            _CanvasView.HorizontalOptions = LayoutOptions.FillAndExpand;
            _CanvasView.VerticalOptions = LayoutOptions.FillAndExpand;

        }

  
        private void OnButtonClicked(object sender, EventArgs e)
        {
            var lm = Manager.ActiveKernel.Get<LayerManager>();
            foreach(var l in lm.Layers)
            {
                l.ClearLayer();
            }
            lm.TempLayer.ClearLayer();
        }

       
        
       
    }
}