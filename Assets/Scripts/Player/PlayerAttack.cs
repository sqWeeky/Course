using Data;
using Infastracture.Services;
using Infastracture.Services.PersistentProgress;
using Logic;
using Services.Input;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerAttack : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private CharacterController _controller;

        private static int _layerMask;


        private IInputService _input;
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();

            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_input.IsAttackButtonUp() && !_animator.IsAttacking)
                _animator.PlayAttack();
        }

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
            }
        }

        public void LoadProgress(PlayerProgress progress) =>
            _stats = progress.PlayerStats;

        public void UpdateProgress(PlayerProgress progress)
        {
        }

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

        private Vector3 StartPoint() =>
            new(transform.position.x, _controller.center.y / 2, transform.position.z);
    }
}