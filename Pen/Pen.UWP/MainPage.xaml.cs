using Ninject;
using Pen.ContextModules;

namespace Pen.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            var kernel = new StandardKernel(new GlobalModule());

            LoadApplication(kernel.Get<Pen.App>());
        }
    }
}
