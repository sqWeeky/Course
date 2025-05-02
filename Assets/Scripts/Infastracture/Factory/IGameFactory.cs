using UnityEngine;

namespace Infastracture.Factory
{
    public interface IGameFactory
    {
        GameObject CreatePlayer(GameObject initialPoint);
        void CreateHud();
    }
}