using Actors.InputThings.AI.States;
using Actors.InputThings.StateMachineThings;
using UnityEngine;

namespace Actors.InputThings.AI
{
    public class SimpleEnemyInput : MonoBehaviour, IActorInput
    {
        public Vector2 Movement { get; private set; }
        public Vector3 TargetPosition { get; private set; }
        public bool Fire { get; private set; }

        private readonly StateMachine _stateMachine = new();
        private WanderState _wanderState;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _wanderState = new WanderState(transform);
        }
    }
}