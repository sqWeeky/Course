using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infastracture.AssetManagement
{
    public class AssetProvider : IAssets
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache =
            new Dictionary<string, AsyncOperationHandle>();

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_completedCache.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;
            
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);

            return await handle.Task;
        }

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