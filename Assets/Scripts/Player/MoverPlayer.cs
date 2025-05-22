using System;
using Data;
using Infastracture.Services;
using Infastracture.Services.PersistentProgress;
using Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class MoverPlayer : MonoBehaviour, ISavedProgress
    {
        private static readonly int s_isRunning = Animator.StringToHash("IsRunning");

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;

        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();

            _camera = Camera.main;
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
               Debug.Log(PlayerPrefs.GetString(SceneManager.GetActiveScene().name)); 
        }

        private void Update()
        {
            if (_inputService.Axis.sqrMagnitude > 0.001f)
                Run();
            else
                _animator.SetBool(s_isRunning, false);
        }


        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel =
                new PositionOnLevel(FindCurrentLevel(), transform.position.AsVectorData());

        public void LoadProgress(PlayerProgress progress)
        {
            if (FindCurrentLevel() == progress.WorldData.PositionOnLevel.LevelName)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;

                if (savedPosition != null)
                    Warp(to: savedPosition);
            }
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

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private string FindCurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}