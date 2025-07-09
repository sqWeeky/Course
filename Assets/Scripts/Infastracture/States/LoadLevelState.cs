using CameraLogic;
using Canvas;
using Infastracture.Factory;
using Infastracture.Services.PersistentProgress;
using Logic;
using Player;
using UnityEngine;

namespace Infastracture.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPlayerPoint";
        private const string EnemySpawnerTag = "EnemySpawner";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;


        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                Debug.Log(progressReader);
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void InitGameWorld()
        {
            InitSpawners();

            var player = InitPlayer();

            InitHub(player);
            CameraFollow(player);
        }

        private void InitSpawners()
        {
            foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
            {
                var spawner = spawnerObject.GetComponent<EnemySpawner>();
                _gameFactory.Register(spawner);
            }
        }


        private void InitHub(GameObject player)
        {
            GameObject hub = _gameFactory.CreateHud();
            hub.transform.SetParent(player.transform);
            hub.GetComponentInChildren<ActorUI>().Construct(player.GetComponent<PlayerHealth>());
        }

        private GameObject InitPlayer() =>
            _gameFactory.CreatePlayer(GameObject.FindWithTag(InitialPointTag));

        private void CameraFollow(GameObject player)
        {
            if (Camera.main != null)
                Camera.main.GetComponent<CameraFollow>().Follow(player);
        }
    }
}