using Infastracture.AssetManagement;
using Infastracture.Services;
using StaticData.Windows;
using UI.Services.Window;
using UnityEngine;

namespace UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticDataService;

        private Transform _uiRoot;

        public UIFactory(IAssets assets, IStaticDataService staticDataService)
        {
            _assets = assets;
            _staticDataService = staticDataService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticDataService.ForWindow(WindowId.Shop);
            Object.Instantiate(config.Prefab, _uiRoot);
        }

        public void CreateUIRoot() =>
            _uiRoot = _assets.Instantiate(Constants.UI.UIRootPath).transform;
    }
}