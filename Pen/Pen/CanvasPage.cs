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
using Pen.UI.CustomControls.ColorSliders;
using Pen.UI.CustomControls.BooleanSliders;
using Pen.UI.CustomControls;

namespace Pen
{
    public class CanvasPage : ContentPage
    {
        public delegate void DrawOnCanvasDelegate(SKPaintSurfaceEventArgs args);
        
        private MainTouchCanvas _CanvasView;
        private ContextManager Manager;
        public CanvasPage(MainTouchCanvas _canvas, ContextManager man, UseFIllController sl, RoundSlider cl)
        {
            Manager = man;
            Title = "SIMPLE CIRCLE";
            _CanvasView = _canvas;
           
           
            var but = new Button();
            but.Clicked += OnButtonClicked;
            but.Text = "CLICK ME";
            var layout = new StackLayout();
            layout.Children.Add(but);
            //layout.Children.Add(_CanvasView);
            
            layout.Children.Add(sl);
            cl.AnchorX = 0;
            cl.AnchorY = 0;
            var slider = new Slider();
           
            layout.Children.Add(slider);
            cl.HorizontalOptions = LayoutOptions.FillAndExpand;
            cl.VerticalOptions = LayoutOptions.FillAndExpand;
            layout.Children.Add(cl);
            Content = layout;

            
            _CanvasView.HorizontalOptions = LayoutOptions.CenterAndExpand;
            _CanvasView.VerticalOptions = LayoutOptions.CenterAndExpand;

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