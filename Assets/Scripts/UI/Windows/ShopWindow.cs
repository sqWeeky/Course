using TMPro;
using UnityEngine;

namespace UI.Windows
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _skullText;

        protected override void Initialize() =>
            Progress.WorldData.LootData.Changed += RefreshSkullText;

        protected override void Cleanup()
        {
            base.Cleanup();
            Progress.WorldData.LootData.Changed -= RefreshSkullText;
        }

        private void RefreshSkullText() =>
            _skullText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}