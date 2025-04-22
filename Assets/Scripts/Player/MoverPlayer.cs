using Infastracture;
using Services.Input;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class MoverPlayer : MonoBehaviour
    {
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private Animator _animator;

        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_inputService.Axis.sqrMagnitude > 0.001f)
                Run();
            else
                _animator.SetBool(IsRunning, false);
        }


        private void Run()
        {
            Vector3 movementVector = Vector3.zero;

            movementVector = _camera.transform.TransformDirection(_inputService.Axis);
            movementVector.y = 0f;
            movementVector.Normalize();

            transform.forward = movementVector;

            _animator.SetBool(IsRunning, true);
            movementVector += Physics.gravity;
            _characterController.Move(_moveSpeed * movementVector * Time.deltaTime);
        }
    }
}