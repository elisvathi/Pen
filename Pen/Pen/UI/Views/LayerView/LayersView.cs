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
        private StackLayout cont = new StackLayout();

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
            var scroll = new ScrollView();
            scroll.Content = cont;
            var layout = new StackLayout
            {
                Children = {
                    but,
                    butCl,
                    new Label { Text = "Layers" },
                    cont
                }
            };

            RefreshLayers();
            Content = layout;
        }
        
        private void RefreshLayers()
        {
            cont.Children.Clear();
            for (int i = lmanager.Layers.Count - 1; i >= 0; i--)
            {
                var l = lmanager.Layers[i];
                //v.Children.Add(new LayerThumbnail(l, manager));
                cont.Children.Add(new LayerThumbnail(l, manager));
            }

        }
        private void Close_window(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(manager.ActiveKernel.Get<MainCanvasPage>());
        }

        private void Create_layer(object sender, EventArgs e)
        {
            lmanager.AddNewLayer();
            RefreshLayers();
            ForceLayout();
        }
    }
}