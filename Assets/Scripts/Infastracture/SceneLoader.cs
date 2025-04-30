using System;
using System.Collections;
using Canvas;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infastracture
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public void Load(string sceneName, Action onLoad = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoad));

        private IEnumerator LoadScene(string nextScene, Action onLoad = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoad?.Invoke();
                yield break;
            }
                
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (waitNextScene is { isDone: false })
                yield return null;
            
            onLoad?.Invoke();
        }
    }
}