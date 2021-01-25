using Prism.Modularity;
using Prism.Regions;

using EndToEndImageReviewApp.Views;
using Unity;
using Prism.Ioc;

namespace EndToEndImageReviewApp
{
    class WorklistModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public WorklistModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _regionManager.RegisterViewWithRegion("WorklistRegion", typeof(WorklistView));
            _regionManager.RegisterViewWithRegion("SidebarRegion", typeof(StaffLoginView));
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // no additional initialization
        }
    }
}
