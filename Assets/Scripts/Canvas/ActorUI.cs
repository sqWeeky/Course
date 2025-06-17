using Logic;
using UnityEngine;

namespace Canvas
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

            //Debug.Log(_health);
        }

        public void Construct(IHealth heath)
        {
            _health = heath;

            _health.HealthChanged += UpdateUPBar;
        }

        private void OnDestroy() =>
            _health.HealthChanged -= UpdateUPBar;

        private void UpdateUPBar()
        {
            _hpBar.SetValue(_health.CurrentHealth, _health.MaxHealth);
        }
    }
}