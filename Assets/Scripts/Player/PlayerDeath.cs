using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private MoverPlayer _mover;
        [SerializeField] private PlayerAttack _attack;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private GameObject _deathFX;

        private bool _isDead;

        private void Start() =>
            _health.HealthChanged += HealthChanged;

        private void OnDestroy() =>
            _health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!_isDead && _health.CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _mover.enabled = false;
            _attack.enabled = false;
            _animator.PlayDeath();

            Instantiate(_deathFX, transform.position, Quaternion.identity);
        }
    }
}