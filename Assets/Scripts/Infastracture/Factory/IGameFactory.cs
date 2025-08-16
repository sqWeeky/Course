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

        Task<GameObject> CreatePlayer(Vector3 initialPoint);
        Task<GameObject> CreateHud();
        void CleanUp();
        Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeID monsterTypeID);

        Task<GameObject> CreateMonster(MonsterTypeID typeID, Transform parent);
        Task<LootPiece> CreatLoot();
        Task WarmUp();
    }
}