using Pen.ContextModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Xamarin.Forms;

namespace Pen.UI.Views
{
    public class TestPage : ContentPage
    {
        private ContextManager manager;
        public TestPage(ContextManager mg)
        {
            manager = mg;
            Content = new StackLayout
            {
                Children = {
                   manager.ActiveKernel.Get<ColorPickerView>()
                }
            };
        }
    }
}