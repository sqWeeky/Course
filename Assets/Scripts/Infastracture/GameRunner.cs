using System;
using UnityEngine;

namespace Infastracture
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;

        private void Awake()
        {
            var bootstraper = FindObjectOfType<GameBootstrapper>();

            if (bootstraper == null)
                Instantiate(BootstrapperPrefab);
        }
    }
}