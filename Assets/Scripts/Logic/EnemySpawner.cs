using System;
using Data;
using Enemy;
using Infastracture.Factory;
using Infastracture.Services;
using Infastracture.Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeID MonsterTypeID;

        [SerializeField] private bool _slain;

        private string _id;
        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;

        public bool Slain => _slain;

        private void Awake()
        {
            _id = GetComponent<UniqueID>().ID;
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id)) 
                _slain = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            GameObject monster = _gameFactory.CreateMonster(MonsterTypeID, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.Happened -= Slay;
            
            _slain = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (Slain)
                progress.KillData.ClearedSpawners.Add(_id);
        }
    }
}