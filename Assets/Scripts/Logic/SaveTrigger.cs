using System;
using Infastracture.Services;
using Infastracture.Services.SaveLoad;
using UnityEngine;

namespace Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        public BoxCollider Collider;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress save");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (Collider != null)
                return;

            Gizmos.color = new Color(30f, 200f, 30f, 130f);
            Gizmos.DrawWireCube(transform.position + Collider.center, Collider.size);
        }
    }
}