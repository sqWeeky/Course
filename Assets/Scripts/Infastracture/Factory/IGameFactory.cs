using System;
using System.Collections.Generic;
using Infastracture.Services;
using Infastracture.Services.PersistentProgress;
using UnityEngine;

namespace Infastracture.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject HeroGameObject { get; }

        event Action HeroCreated;
        GameObject CreatePlayer(GameObject initialPoint);

        GameObject CreateHud();
        void CleanUp();
    }
}