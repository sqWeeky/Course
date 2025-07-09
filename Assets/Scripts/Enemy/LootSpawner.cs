using Data;
using Infastracture.Factory;
using Infastracture.Services.Randomizer;
using Logic;
using UnityEngine;

namespace Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;

        private IGameFactory _factory;
        private IRandomService _random;

        private int _lootMin;
        private int _lootMax;

        private void Start()
        {
            EnemyDeath.Happened += SpawnLoot;
        }

        public void Construct(IGameFactory factory, IRandomService random)
        {
            _factory = factory;
            _random = random;
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }

        private void SpawnLoot()
        {
            EnemyDeath.Happened -= SpawnLoot;

            LootPiece lootPiece = _factory.CreatLoot();
            lootPiece.transform.position = transform.position;
            lootPiece.GetComponent<UniqueID>().GenerateId();

            Loot lootItem = GenerateLootItem();
            lootPiece.Initialize(lootItem);
        }

        private Loot GenerateLootItem()
        {
            Loot loot = new Loot
            {
                Value = _random.Next(_lootMin, _lootMax)
            };

            return loot;
        }
    }
}