using UnityEngine;

namespace Actors.InputThings.StateMachineThings
{
    public abstract class State
    {
        public IActorInput Actor;

        protected State(Transform actor)
        {
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Execute();
    }
}