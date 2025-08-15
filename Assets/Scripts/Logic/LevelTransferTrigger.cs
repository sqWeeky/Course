using System;
using Infastracture.Services;
using Infastracture.States;
using UnityEngine;

namespace Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        public string TransferTo;

        private IGameStateMachine _stateMachine;
        private bool _triggered;

        private void Awake() // Сделать через конструктор и фабрику
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_triggered)
                return;

            if (other.CompareTag(Constants.Tag.PlayerTag))
            {
                _stateMachine.Enter<LoadLevelState, string>(TransferTo);
                _triggered = true;
            }
        }
    }
}