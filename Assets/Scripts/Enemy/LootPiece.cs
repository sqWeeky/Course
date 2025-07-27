using System.Collections;
using System.Collections.Generic;
using Data;
using Infastracture.Services.PersistentProgress;
using Logic;
using TMPro;
using UnityEngine;

namespace Enemy
{
    public class LootPiece : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private GameObject _skullPrefab;
        [SerializeField] private GameObject _pickUpFXPrefab;
        [SerializeField] private GameObject _pickUpPopupPrefab;
        [SerializeField] private TextMeshPro _textLoot;

        private Loot _loot;
        private WorldData _worldData;

        private string _id;

        private bool _picked;
        private bool _loadedFromProgress;

        private void Start()
        {
            if (!_loadedFromProgress)
                _id = GetComponent<UniqueID>().ID;
        }

        public void Construct(WorldData worldData) =>
            _worldData = worldData;

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _id = GetComponent<UniqueID>().ID;

            LootPieceData data = progress.WorldData.LootData.LootPiecesOnScene.Get(_id);
            Initialize(data.Loot);
            transform.position = data.Position.AsUnityVector();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_picked)
                return;

            LootPieceDataDictinary lootPiecesOnScene = progress.WorldData.LootData.LootPiecesOnScene;

            if (!lootPiecesOnScene.Contains(_id))
                lootPiecesOnScene.Add(_id, new LootPieceData(transform.position.AsVectorData(), _loot));
        }

        private void OnTriggerEnter(Collider other) =>
            PickUp();

        private void PickUp()
        {
            if (!_picked)
            {
                _picked = true;

                UpdateWorldData();
                HideSkull();
                PlayPickUpFX();
                ShowText();

                StartCoroutine(StartDestroyTimer());
            }
        }

        private void UpdateWorldData()
        {
            UpdateCollectedLootAmount();
            RemoveLootPieceFromSavedPieces();
        }

        private void RemoveLootPieceFromSavedPieces()
        {
            LootPieceDataDictinary savedLootPieces = _worldData.LootData.LootPiecesOnScene;

            if (savedLootPieces.Contains(_id))
                savedLootPieces.Remove(_id);
        }

        private void UpdateCollectedLootAmount() =>
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