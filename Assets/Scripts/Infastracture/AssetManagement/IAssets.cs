using System.Threading.Tasks;
using Infastracture.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infastracture.AssetManagement
{
    public interface IAssets : IService
    {
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        Task<T> Load<T>(string address) where T : class;
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        void CleanUp();
        void Initialize();
    }
}