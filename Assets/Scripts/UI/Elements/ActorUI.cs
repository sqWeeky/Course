using Logic;
using UnityEngine;

namespace UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private IHealth _health;

        private void Awake()
        {
            IHealth health = GetComponentInParent<IHealth>();

            if (health != null)
                Construct(health);
        }

        public void Construct(IHealth heath)
        {
            _health = heath;

            _health.HealthChanged += UpdateUpBar;
        }

        private void OnDestroy() =>
            _health.HealthChanged -= UpdateUpBar;

        private void UpdateUpBar()
        {
            _hpBar.SetValue(_health.CurrentHealth, _health.MaxHealth);
        }
    }
}