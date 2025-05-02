using UnityEngine;

namespace Infastracture.AssetManagement
{
    public interface IAssets
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}