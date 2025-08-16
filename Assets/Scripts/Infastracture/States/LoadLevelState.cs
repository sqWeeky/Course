using CameraLogic;
using Enemy;
using Infastracture.Factory;
using Infastracture.Services;
using Infastracture.Services.PersistentProgress;
using Logic;
using Player;
using StaticData;
using UI.Elements;
using UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infastracture.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string EnemySpawnerTag = "EnemySpawner";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticDataService,
            IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            _gameFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            IniUIRoot();
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void IniUIRoot() =>
            _uiFactory.CreateUIRoot();

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private void InitGameWorld()
        {
            var levelData = LevelStaticData();

            InitSpawners(levelData);
            InitLootPieces();

            var player = InitPlayer(levelData);

            InitHub(player);
            CameraFollow(player);
        }

        private void InitSpawners(LevelStaticData levelData)
        {
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
                _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.MonsterTypeID);
        }

        private void InitLootPieces()
        {
            foreach (string key in _progressService.Progress.WorldData.LootData.LootPiecesOnScene.Id)
            {
                LootPiece lootPiece = _gameFactory.CreatLoot();
                lootPiece.GetComponent<UniqueID>().ID = key;
            }
        }

        private void InitHub(GameObject player)
        {
            GameObject hub = _gameFactory.CreateHud();
            hub.transform.SetParent(player.transform);
            hub.GetComponentInChildren<ActorUI>().Construct(player.GetComponent<PlayerHealth>());
        }

        private GameObject InitPlayer(LevelStaticData levelData) =>
            _gameFactory.CreatePlayer(levelData.InitialHeroPosition);

        private LevelStaticData LevelStaticData()
        {
            string sceneKye = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticDataService.ForLevel(sceneKye);
            return levelData;
        }

        private void CameraFollow(GameObject player)
        {
            if (Camera.main != null)
                Camera.main.GetComponent<CameraFollow>().Follow(player);
        }
    }
}