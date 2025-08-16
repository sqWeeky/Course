using Data;
using Enemy;
using Infastracture.Factory;
using Infastracture.Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private bool _slain;

        public MonsterTypeID MonsterTypeID;

        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;

        public string Id { get; set; }
        private bool Slain => _slain;

        public void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
                _slain = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (Slain)
                progress.KillData.ClearedSpawners.Add(Id);
        }

        private async void Spawn()
        {
            GameObject monster = await _gameFactory.CreateMonster(MonsterTypeID, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.Happened -= Slay;

            _slain = true;
        }
    }
}