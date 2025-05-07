using System.Collections.Generic;
using Infastracture.Services;
using Infastracture.Services.PersistentProgress;
using UnityEngine;

namespace Infastracture.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(GameObject initialPoint);
        void CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CleanUp();
    }
}