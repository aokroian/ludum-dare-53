using UnityEngine;

namespace Actors.InputThings
{
    public interface IActorInput
    {
        public Vector2 Movement { get; }
        public Vector3 TargetPosition { get; } 
        
        public bool Fire { get; }
    }
}