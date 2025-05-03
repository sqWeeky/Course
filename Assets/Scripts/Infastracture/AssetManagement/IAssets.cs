using Infastracture.Services;
using UnityEngine;

namespace Infastracture.AssetManagement
{
    public interface IAssets: IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}