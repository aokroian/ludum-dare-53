using UnityEngine;

namespace Actors.InputThings.StateMachineThings
{
    public abstract class State
    {
        public abstract void Enter();

        public abstract void Exit();

        public abstract void Execute();
    }
}