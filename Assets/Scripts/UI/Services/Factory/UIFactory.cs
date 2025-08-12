using Infastracture.AssetManagement;
using Infastracture.Services;
using Infastracture.Services.PersistentProgress;
using StaticData.Windows;
using UI.Services.Window;
using UI.Windows;
using UnityEngine;

namespace UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _progressService;

        private Transform _uiRoot;

        public UIFactory(IAssets assets, IStaticDataService staticDataService, IPersistentProgressService progressService)
        {
            _assets = assets;
            _staticDataService = staticDataService;
            _progressService = progressService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticDataService.ForWindow(WindowId.Shop);
            WindowBase window= Object.Instantiate(config.Prefab, _uiRoot);
            window.Construct(_progressService);
        }

        public void CreateUIRoot() =>
            _uiRoot = _assets.Instantiate(Constants.UI.UIRootPath).transform;
    }
}