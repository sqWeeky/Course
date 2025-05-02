using Infastracture.AssetManagement;
using UnityEngine;

namespace Infastracture.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreatePlayer(GameObject initialPoint) =>
            _assets.Instantiate(Constants.AssetPath.PlayerPath, initialPoint.transform.position);

        public void CreateHud() =>
            _assets.Instantiate(Constants.AssetPath.HubPath);
    }
}