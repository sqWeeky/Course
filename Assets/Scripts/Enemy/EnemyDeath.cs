using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAnimator), typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private GameObject _deathFX;

        private readonly float _delayDeath = 3f;

        public event Action Happened;

        private void Start() =>
            _health.HealthChanged += HealthChanged;

        private void OnDisable() =>
            _health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (_health.CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            _health.HealthChanged -= HealthChanged;
            
            _animator.PlayDeath();

            SpawnDeathFX();
            StartCoroutine(DestroyTimer());

            Happened?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(_delayDeath);
            Destroy(gameObject);
        }

        private void SpawnDeathFX() =>
            Instantiate(_deathFX, transform.position, Quaternion.identity);
    }
}