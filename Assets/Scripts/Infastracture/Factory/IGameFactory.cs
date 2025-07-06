using System.Collections.Generic;
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
        
        GameObject CreatePlayer(GameObject initialPoint);
        GameObject CreateHud();
        void CleanUp();
        void Register(ISavedProgressReader savedProgress);
        GameObject CreateMonster(MonsterTypeID typeID, Transform parent);
        LootPiece CreatLoot();
    }
}