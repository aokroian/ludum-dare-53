using System;
using Actors.InputThings.StateMachineThings;
using UnityEngine;

namespace Actors.InputThings.AI.States
{
    public class WanderState : State
    {
        private Transform _actorTransform;
        private readonly Action<Vector2> _setMovementAction;
        private readonly Action<Vector3> _setLookAction;
        private readonly float _timeToWander;
        private readonly float _timeToIdle;

        private float _wanderStartTime;
        private float _idleStartTime;
        private bool _isIdle;
        private Vector2 _prevMovement;
        private Vector3 _prevLook;

        public WanderState(
            Transform actorTransform,
            Action<Vector2> setMovementAction,
            Action<Vector3> setLookAction,
            float timeToWander = 1f,
            float timeToIdle = 1f
        )
        {
            _actorTransform = actorTransform;
            _setMovementAction = setMovementAction;
            _setLookAction = setLookAction;
            _timeToWander = timeToWander;
            _timeToIdle = timeToIdle;

            _isIdle = true;
            _idleStartTime = Time.time - timeToIdle;
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Execute()
        {
            var time = Time.time;
            var movement = _prevMovement;
            var look = _prevLook;
            if (_isIdle)
            {
                if (time - _idleStartTime > _timeToIdle)
                {
                    _isIdle = false;
                    _wanderStartTime = time;
                    movement = UnityEngine.Random.insideUnitCircle.normalized; 
                    look = _actorTransform.position + new Vector3(movement.x, 0f, movement.y);
                }
            }
            else
            {
                if (time - _wanderStartTime > _timeToWander)
                {
                    _isIdle = true;
                    _idleStartTime = time;
                    movement = Vector2.zero;
                }
            }

            _setMovementAction.Invoke(movement);
            _setLookAction.Invoke(look);
            _prevMovement = movement;
            _prevLook = look;
        }
    }
}