using Actors.InputThings.AI.States;
using UnityEngine;

namespace Actors.InputThings.AI
{
    public class SimpleWanderingEnemyBrain : AIActorInput
    {
        [SerializeField] [Range(.5f, 10f)] private float timeToWander = 1f;
        [SerializeField] [Range(.5f, 10f)] private float timeToIdle = 1f;

        private WanderState _wanderState;

        private void Awake()
        {
            InitStates();
        }

        private void InitStates()
        {
            _wanderState = new WanderState(transform, SetMovement, SetLook, timeToWander, timeToIdle);
        }

        private void Update()
        {
            StateMachine.CurrentState = _wanderState;
            StateMachine.ExecuteCurrentState();
        }
    }
}