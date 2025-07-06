using Data;
using Infastracture.Factory;
using Infastracture.Services.Randomizer;
using UnityEngine;

namespace Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;

        private IGameFactory _factory;
        private IRandomService _random;

        private int _lootMax;
        private int _lootMin;

        private void Start()
        {
            EnemyDeath.Happened += SpawnLoot;
        }

        public void Construct(IGameFactory factory, IRandomService random)
        {
            _factory = factory;
            _random = random;
        }

        private void SpawnLoot()
        {
            LootPiece loot = _factory.CreatLoot();
            loot.transform.position = transform.position;

            Loot lootItem = GenerateLootItem();
            loot.Initialize(lootItem);
        }

        private Loot GenerateLootItem()
        {
            return new Loot()
            {
                Value = _random.Next(_lootMin, _lootMax),
            };
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}