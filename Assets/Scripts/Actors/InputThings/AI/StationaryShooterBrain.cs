using System;
using Actors.InputThings.AI.States;
using UnityEngine;

namespace Actors.InputThings.AI
{
    public class StationaryShooterBrain : AIActorInput
    {
        private FireState _fireState;

        private bool _isInit;
        private Transform _playerTransform;

        private void Awake()
        {
            _playerTransform = LocatePlayer();
        }

        private void Init()
        {
            if (_playerTransform == null)
                throw new Exception("Player Transform not found");
            _fireState = new FireState(
                _playerTransform,
                0.5f,
                SetLook,
                SetFire);
            _isInit = true;
        }

        private Transform LocatePlayer()
        {
            return FindObjectOfType<PlayerActorInput>().transform;
        }

        private void Update()
        {
            if (!_isInit)
            {
                if (WalkArea != null)
                    Init();
                else
                    return;
            }

            StateMachine.CurrentState = _fireState;
            StateMachine.ExecuteCurrentState();
        }
    }
}