using UnityEngine;

namespace Enemy
{
    public class RotateToPlayer : Follow
    {
        [SerializeField] private float _rotationSpeed = 20f;

        private Transform _playerTransform;
        private Vector3 _positionToLook;
       
        private void Update()
        {
            if (Initialized())
            {
                RotateTowardsPlayer();
            }
        }
        
        public void Construct(Transform heroTransform) =>
            _playerTransform = heroTransform;

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

        }
}