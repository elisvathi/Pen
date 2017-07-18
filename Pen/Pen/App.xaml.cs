using Ninject;
using Pen.ContextModules;
using Pen.UI.MainCanvas;
using Pen.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Pen
{
    public partial class App : Application
    {
        private ContextManager _manager;
        public App(ContextManager manager)
        {
            _manager = manager;
            InitializeComponent();

            MainPage = manager.ActiveKernel.Get<MainCanvasPage>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
