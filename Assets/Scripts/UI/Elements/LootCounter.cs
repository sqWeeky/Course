using Data;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class LootCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        private WorldData _worldData;

        private void Start() =>
            UpdateCounter();

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
            _worldData.LootData.Changed += UpdateCounter;
        }

        private void UpdateCounter() =>
            _counter.text = $"{_worldData.LootData.Collected}";
    }
}