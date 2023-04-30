using Actors.InputThings.AI.States;
using Actors.InputThings.StateMachineThings;
using UnityEngine;

namespace Actors.InputThings.AI
{
    public class SimpleEnemyInput : MonoBehaviour, IActorInput
    {
        [SerializeField] [Range(.5f, 10f)] private float timeToWander = 1f;
        [SerializeField] [Range(.5f, 10f)] private float timeToIdle = 1f;

        public Vector2 Movement { get; private set; }
        public Vector3 Look { get; private set; }
        public bool Fire { get; private set; }

        private readonly StateMachine _stateMachine = new();
        private WanderState _wanderState;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _wanderState = new WanderState(transform, SetMovement, SetLook, timeToWander, timeToIdle);
        }

        private void Update()
        {
            _stateMachine.CurrentState = _wanderState;
            _stateMachine.ExecuteCurrentState();
        }


        private void SetMovement(Vector2 movement)
        {
            Movement = movement;
        }

        private void SetLook(Vector3 look)
        {
            Look = look;
        }
    }
}