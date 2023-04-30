using Actors.InputThings;
using UnityEngine;

namespace Actors
{
    public class ActorSystem : MonoBehaviour
    {
        public IActorInput ActorInput { get; private set; }

        private void Awake()
        {
            ActorInput = GetComponent<IActorInput>();
        }
    }
}