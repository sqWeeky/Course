using System;
using Data;
using Infastracture.Services.PersistentProgress;
using Logic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        [SerializeField] private PlayerAnimator _animator;

        private State _state;

        public event Action HealthChanged;
        
        public float CurrentHealth
        {
            get => _state.CurrentHP;
            set
            {
                if (!Mathf.Approximately(value, _state.CurrentHP))
                {
                    _state.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float MaxHealth
        {
            get => _state.MaxHP;
            set => _state.MaxHP = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHP = CurrentHealth;
            progress.HeroState.MaxHP = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHealth <= 0)
                return;

            CurrentHealth -= damage;
            _animator.PlayHit();
        }
    }
}