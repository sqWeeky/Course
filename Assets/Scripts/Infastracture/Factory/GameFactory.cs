using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Enemy;
using Infastracture.AssetManagement;
using Infastracture.Services;
using Infastracture.Services.PersistentProgress;
using Infastracture.Services.Randomizer;
using Logic;
using Logic.EnemySpawners;
using StaticData;
using UI;
using UI.Elements;
using UI.Services.Window;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Infastracture.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _progressService;
        private readonly IWindowService _windowService;

        public GameFactory(IAssets assets, IStaticDataService staticData, IRandomService randomService,
            IPersistentProgressService persistentProgressService, IWindowService windowService)
        {
            _assets = assets;
            _staticData = staticData;
            _randomService = randomService;
            _progressService = persistentProgressService;
            _windowService = windowService;
        }

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject HeroGameObject { get; set; }

        public async Task WarmUp()
        {
            // await _assetProvide.Load<GameObject>(Constants.AssetAddress.Loot);
            // await _assetProvide.Load<GameObject>(Constants.AssetAddress.Spawner);
        }

        public LootPiece CreatLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(Constants.AssetAddress.Loot).GetComponent<LootPiece>();
            lootPiece.Construct(_progressService.Progress.WorldData);
            return lootPiece;
        }

        public GameObject CreatePlayer(Vector3 initialPoint)
        {
            HeroGameObject = InstantiateRegistered(Constants.AssetAddress.PlayerPath, initialPoint);
            return HeroGameObject;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(Constants.AssetAddress.HubPath);
            hud.GetComponentInChildren<LootCounter>().Construct(_progressService.Progress.WorldData);

            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Construct(_windowService);
            }

            return hud;
        }

        public void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeID monsterTypeID)
        {
            SpawnPoint spawner = InstantiateRegistered(Constants.AssetAddress.Spawner)
                .GetComponent<SpawnPoint>();

            spawner.Construct(this);
            spawner.Id = spawnerId;
            spawner.MonsterTypeID = monsterTypeID;
            spawner.transform.position = at;
        }

        public async Task<GameObject> CreateMonster(MonsterTypeID typeID, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(typeID);

            GameObject prefab = await _assets.Load<GameObject>(monsterData.PrefabReference);
            GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity,
                parent);

            IHealth health = monster.GetComponent<IHealth>();
            health.CurrentHealth = monsterData.Health;
            health.MaxHealth = monsterData.Health;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);
            lootSpawner.Construct(this, _randomService);

            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;
            attack.Cleavage = monsterData.Cleavage;
            attack.Cooldown = monsterData.CooldownAttack;

            monster.GetComponent<RotateToPlayer>()?.Construct(HeroGameObject.transform);

            return monster;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}