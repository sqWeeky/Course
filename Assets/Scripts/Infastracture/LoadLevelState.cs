using CameraLogic;
using Canvas;
using UnityEngine;

namespace Infastracture
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPlayerPoint";
        private const string PlayerPath = "Player";
        private const string HubPath = "Hub/Hud";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            var initialPoint = GameObject.Find(InitialPointTag);

            var player = Instantiate(PlayerPath, initialPoint.transform.position);
            Instantiate(HubPath);
            CameraFollow(player);
            
            _stateMachine.Enter<GameLoopState>();
        }

        private static void CameraFollow(GameObject player)
        {
            if (Camera.main != null)
                Camera.main.GetComponent<CameraFollow>().Follow(player);
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);

            if (prefab == null)
            {
                Debug.LogError($"[LoadLevelState] Cannot find prefab at Resources/{path}");
                return null;
            }

            return Object.Instantiate(prefab);
        }

        private static GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);

            if (prefab == null)
            {
                Debug.LogError($"[LoadLevelState] Cannot find prefab at Resources/{path}");
                return null;
            }

            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}