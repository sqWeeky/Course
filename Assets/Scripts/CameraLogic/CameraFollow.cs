using UnityEngine;

namespace CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _rotationAngleX = 30f;
        [SerializeField] private int _distance = 5;
        [SerializeField] private float _offsetY = 1f;
        [SerializeField] private Transform _following;

        private void LateUpdate()
        {
            if (!_following)
                return;

            Quaternion rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();
            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject following)
        {
            _following = following.transform;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += _offsetY;
            return followingPosition;
        }
    }
}