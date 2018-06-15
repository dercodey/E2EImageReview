using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

using EndToEndImageReviewApp.Views;

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

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("BlockLayoutRegion", typeof(ImageViewerView));
        }
    }
}
