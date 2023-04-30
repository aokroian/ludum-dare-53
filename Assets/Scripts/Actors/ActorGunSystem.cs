using Actors.Combat;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actors
{
    public class ActorGunSystem : ActorSystem
    {
        [SerializeField] private float gunDistance = 0.6f;
        [SerializeField] private Gun gun;

        private void Update()
        {
            RotateGunAroundPlayer();
            if (ActorInput.Fire)
                gun.Fire();
        }


        private void RotateGunAroundPlayer()
        {
            var position = transform.position;
            var mousePosition = ActorInput.Look;
            var direction = (mousePosition - position).normalized;

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gun.transform.rotation = Quaternion.Euler(0, 0, angle);

            var gunPosition = position + Quaternion.Euler(0.4f, 0, angle) * Vector3.right * gunDistance;
            gunPosition.z = -1;
            gunPosition.y -= 0.2f;
            gun.transform.position = gunPosition;
            gun.FlipSprite(angle);
        }
    }
}