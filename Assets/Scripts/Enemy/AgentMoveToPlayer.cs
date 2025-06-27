using Infastracture.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentMoveToPlayer : Follow
    {
        private const float MinimalDistance = 1f;

        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private void Update() => 
            SetDestinationForAgent();

        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void SetDestinationForAgent()
        {
            if (Initialized() && HeroNotReached())
                _agent.destination = _heroTransform.position;
        }

        private bool Initialized() =>
            _heroTransform != null;

        private bool HeroNotReached() =>
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= MinimalDistance;
    }
}