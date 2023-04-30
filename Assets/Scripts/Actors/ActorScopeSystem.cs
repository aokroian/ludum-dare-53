
using UnityEngine;

namespace Actors
{
    public class ActorScopeSystem : ActorSystem
    {
        [SerializeField] private Transform scopeTransform;

        private void Update()
        {
            scopeTransform.transform.position = ActorInput.TargetPosition;
        }
    }
}