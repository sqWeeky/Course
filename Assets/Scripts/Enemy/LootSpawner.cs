using System;
using Infastracture.Factory;
using UnityEngine;

namespace Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;
        private IGameFactory _factory;

        private void Start()
        {
            EnemyDeath.Happened += SpawnLoot;
        }

        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }

        private void SpawnLoot()
        {
            GameObject loot = _factory.CreatLoot();
            loot.transform.position = transform.position;
        }
    }
}