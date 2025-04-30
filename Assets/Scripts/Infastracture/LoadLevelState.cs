using CameraLogic;
using UnityEngine;

namespace Infastracture
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName) =>
            _sceneLoader.Load(sceneName, OnLoaded);

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            Debug.Log("Loading level");
            GameObject player = Instantiate("Player"); // без Resources/
            
            if (player == null)
            {
                Debug.LogError("Player prefab not found!");
                return;
            }
    
            Debug.Log(player.name);
            
            Instantiate("Hub/Hud");

            CameraFollow(player);
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
            
            return Object.Instantiate(prefab);        }
    }
}