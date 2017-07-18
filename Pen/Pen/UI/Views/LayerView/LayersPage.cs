using Pen.ContextModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Xamarin.Forms;

namespace Pen.UI.Views.LayerView
{
    public class LayersPage : ContentPage
    {
        public LayersPage(ContextManager mg)
        {
            Content = new StackLayout
            {
                Children = {
                    mg.ActiveKernel.Get<LayersView>()
                }
            };
        }
    }
}