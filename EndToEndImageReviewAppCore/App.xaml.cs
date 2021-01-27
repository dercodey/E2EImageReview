using Prism.Ioc;
using Prism.Unity;
using System.Windows;

namespace EndToEndImageReviewAppCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return new EndToEndImageReviewApp.MainWindow();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // no types to register
        }
    }
}
