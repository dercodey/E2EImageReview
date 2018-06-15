using Prism.Modularity;
using Prism.Regions;

using Microsoft.Practices.Unity;

using EndToEndImageReviewApp.Views;

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

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("WorklistRegion", typeof(WorklistView));
            _regionManager.RegisterViewWithRegion("SidebarRegion", typeof(StaffLoginView));
        }
    }
}
