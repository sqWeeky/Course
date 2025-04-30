using System.Collections;
using UnityEngine;

namespace Canvas
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;

        private readonly float _delay = 0.03f;
        private readonly float _curtainAlpha = 0.03f;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1f;
        }

        public void Hide() =>
            StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= _curtainAlpha;
                yield return new WaitForSeconds(_delay);
            }

            gameObject.SetActive(false);
        }
    }
}