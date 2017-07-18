using Pen.ContextModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Xamarin.Forms;
using Pen.UI.Views;
using Pen.UI.Views.LayerView;

namespace Pen.UI.MainCanvas
{
    public class MainCanvasPage : ContentPage
    {
        private ContextManager manager;
        public MainCanvasPage(ContextManager man)
        {
            manager = man;
            var but = new Button();
            but.Text = "CHANGE COLOR";
            but.Clicked += Change_page;
            var but2 = new Button();
            but2.Text = "LAYERS";
            but2.Clicked += GoToLayers;
            InputTransparent = true;
            man.ActiveKernel.Get<MainTouchCanvas>().VerticalOptions = LayoutOptions.FillAndExpand;
            Content = new StackLayout
            {

                Children = {
                    new Label { Text = "Canvas Page" },
                    but,
                    but2,
                    manager.ActiveKernel.Get<MainTouchCanvas>(),
                    
                }
            };
        }

        private void GoToLayers(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(manager.ActiveKernel.Get<LayersPage>());
        }

        private void Change_page(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(manager.ActiveKernel.Get<TestPage>());
        }
    }
}