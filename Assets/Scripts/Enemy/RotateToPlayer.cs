using Infastracture.Factory;
using Infastracture.Services;
using UnityEngine;

namespace Enemy
{
    public class RotateToPlayer : Follow
    {
        [SerializeField] private float _rotationSpeed = 20f;

        private Transform _playerTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (PlayerExists())
            {
                InitializePlayerTransform();
            }
            else
            {
                _gameFactory.HeroCreated += InitializePlayerTransform;
            }
        }

        private void Update()
        {
            if (Initialized())
            {
                RotateTowardsPlayer();
            }
        }

        private void RotateTowardsPlayer()
        {
            UpdatePositionToLookAt();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 directionToLookAt = _playerTransform.position - transform.position;
            _positionToLook = new Vector3(directionToLookAt.x, directionToLookAt.y, directionToLookAt.z);
        }

        private Quaternion SmoothedRotation(Quaternion transformRotation, Vector3 positionToLook) =>
            Quaternion.Lerp(transformRotation, TargetRotation(positionToLook), SpeedFactor());

        private Quaternion TargetRotation(Vector3 positionToLook) =>
            Quaternion.LookRotation(positionToLook);

        private float SpeedFactor() =>
            _rotationSpeed * Time.deltaTime;

        private bool Initialized() =>
            _playerTransform != null;

        private void InitializePlayerTransform() =>
            _playerTransform = _gameFactory.HeroGameObject.transform;

        private bool PlayerExists() =>
            _gameFactory.HeroGameObject != null;
    }
}