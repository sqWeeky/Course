using System.Collections.Generic;
using System.Threading.Tasks;
using Enemy;
using Infastracture.Services;
using Infastracture.Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Infastracture.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        GameObject CreatePlayer(Vector3 initialPoint);
        GameObject CreateHud();
        void CleanUp();
        void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeID monsterTypeID);

        Task<GameObject> CreateMonster(MonsterTypeID typeID, Transform parent);
        LootPiece CreatLoot();
        Task WarmUp();
    }
}