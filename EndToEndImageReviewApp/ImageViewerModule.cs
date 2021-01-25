// using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

using EndToEndImageReviewApp.Views;
using Unity;
using Prism.Ioc;

namespace EndToEndImageReviewApp
{
    class ImageViewerModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ImageViewerModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _regionManager.RegisterViewWithRegion("BlockLayoutRegion", typeof(ImageViewerView));
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // no additional initialization
        }
    }
}
