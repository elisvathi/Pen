using Pen.ContextModules;
using Pen.Layering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Xamarin.Forms;
using Pen.UI.MainCanvas;

namespace Pen.UI.Views.LayerView
{
    public class LayersView : ContentView
    {
        private LayerManager lmanager;
        private ContextManager manager;
        public LayersView(LayerManager lmg, ContextManager mg )
        {
            lmanager = lmg;
            manager = mg;
            var but = new Button()
            {
                Text = "Add Layer"
            };
            but.Clicked += Create_layer;
            var butCl = new Button()
            {
                Text = "Close"
            };
            butCl.Clicked += Close_window;
            var layout = new StackLayout
            {
                Children = {
                    but,
                    butCl,
                    new Label { Text = "Layers" },
                }
            };
            
            foreach (var l in lmanager.Layers)
            {
                layout.Children.Add(new LayerThumbnail(l, manager));
            }
            Content = layout;
        }

        private void Close_window(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(manager.ActiveKernel.Get<MainCanvasPage>());
        }

        private void Create_layer(object sender, EventArgs e)
        {
            lmanager.AddNewLayer();
            ForceLayout();
        }
    }
}