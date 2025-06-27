using System.Linq;
using Infastracture.Factory;
using Logic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;

        public float Cooldown;
        public float Cleavage;
        public float EffectiveDistance;
        public float Damage;

        private IGameFactory _gameFactory;
        private Transform _playerTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        public void Construct(Transform heroTransform) =>
            _playerTransform = heroTransform;

        public void DisableAttack() =>
            _attackIsActive = false;

        public void EnableAttack() =>
            _attackIsActive = true;

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1f);
                Debug.Log("Attack");
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitCount > 0;
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
                   transform.forward * EffectiveDistance;
        }

        private void OnAttackEnded()
        {
            _attackCooldown = Cooldown;
            _isAttacking = false;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        private void StartAttack()
        {
            transform.LookAt(_playerTransform);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private bool CanAttack() =>
            _attackIsActive && CooldownIsUp() && !_isAttacking;

        private bool CooldownIsUp() =>
            _attackCooldown <= 0;
    }
}