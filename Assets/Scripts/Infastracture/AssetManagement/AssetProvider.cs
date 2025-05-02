using UnityEngine;

namespace Infastracture.AssetManagement
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);

            if (prefab == null)
            {
                Debug.LogError($"[LoadLevelState] Cannot find prefab at Resources/{path}");
                return null;
            }

            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);

            if (prefab == null)
            {
                Debug.LogError($"[LoadLevelState] Cannot find prefab at Resources/{path}");
                return null;
            }

            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}