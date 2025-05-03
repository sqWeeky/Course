using Infastracture.Services;
using UnityEngine;

namespace Infastracture.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(GameObject initialPoint);
        void CreateHud();
    }
}