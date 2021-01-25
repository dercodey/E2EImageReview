using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EndToEndImageReviewApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // no types to register
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            var worklistModuleInfo = new ModuleInfo("WorklistModule", typeof(WorklistModule).AssemblyQualifiedName);
            moduleCatalog.AddModule(worklistModuleInfo);

            var viewerModuleInfo = new ModuleInfo("ImageViewerModule", typeof(ImageViewerModule).AssemblyQualifiedName);
            moduleCatalog.AddModule(viewerModuleInfo);
        }

        protected override Window CreateShell()
        {
            return new Shell();
        }

        protected override void InitializeShell(Window shellWindow)
        {
            base.InitializeShell(shellWindow);
            App.Current.MainWindow = shellWindow;
            App.Current.MainWindow.Show();
        }
    }
}
