using Infastracture;
using Services.Input;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class MoverPlayer : MonoBehaviour
    {
        private static readonly int s_isRunning = Animator.StringToHash("IsRunning");

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;

        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = Game.InputService;

            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }
        
        private void Update()
        {
            if (_inputService.Axis.sqrMagnitude > 0.001f)
                Run();
            else
                _animator.SetBool(s_isRunning, false);
        }


        private void Run()
        {
            var movementVector = _camera.transform.TransformDirection(_inputService.Axis);
            movementVector.y = 0f;
            movementVector.Normalize();

            transform.forward = movementVector;

            _animator.SetBool(s_isRunning, true);
            movementVector += Physics.gravity;
            _characterController.Move(movementVector * (Constants.PlayerSettings.MoveSpeed * Time.deltaTime));
        }
    }
}