using System.Collections;
using Data;
using TMPro;
using UnityEngine;

namespace Enemy
{
    public class LootPiece : MonoBehaviour
    {
        [SerializeField] private GameObject _skullPrefab;
        [SerializeField] private GameObject _pickUpFXPrefab;
        [SerializeField] private GameObject _pickUpPopupPrefab;
        [SerializeField] private TextMeshPro _textLoot;

        private Loot _loot;
        private WorldData _worldData;

        private bool _picked;

        public void Construct(WorldData worldData) =>
            _worldData = worldData;

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other) =>
            PickUp();

        private void PickUp()
        {
            if (_picked)
                return;

            _picked = true;

            UpdateWorldData();
            HideSkull();
            PlayPickUpFX();
            ShowText();

            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateWorldData() =>
            _worldData.LootData.Collect(_loot);

        private void HideSkull() =>
            _skullPrefab.SetActive(false);

        private void PlayPickUpFX() =>
            Instantiate(_pickUpPopupPrefab, transform.position, Quaternion.identity);

        private void ShowText()
        {
            _textLoot.text = $"{_loot.Value}";
            _pickUpPopupPrefab.SetActive(true);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1.5f);

            Destroy(gameObject);
        }
    }
}