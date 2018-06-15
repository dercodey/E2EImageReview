using System.Windows;
using Prism.Modularity;
using Prism.Unity;

namespace EndToEndImageReviewApp
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var worklistModuleInfo = new ModuleInfo("WorklistModule", typeof(WorklistModule).AssemblyQualifiedName);
            this.ModuleCatalog.AddModule(worklistModuleInfo);

            var viewerModuleInfo = new ModuleInfo("ImageViewerModule", typeof(ImageViewerModule).AssemblyQualifiedName);
            this.ModuleCatalog.AddModule(viewerModuleInfo);

        }
    }
}
