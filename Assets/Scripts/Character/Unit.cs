using FSM;
using Spawn;
using UnityEngine;
using Inputs;
using UnityEngine.AI;

namespace Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Unit : PooledComponent
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private NavMeshAgent _mover;

        private StateMachine _stateMachine;
        
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _mover.GetComponent<NavMeshAgent>();
        }
    }
}