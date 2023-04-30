using UnityEngine;

namespace Actors
{
    public class ActorGunSystem : ActorSystem
    {
        [SerializeField] private float gunDistance = 0.6f;
        [SerializeField] private Transform gunTransform;


        public void Update()
        {
            RotateGunAroundPlayer();
        }

        private void RotateGunAroundPlayer()
        {
            var position = transform.position;
            var mousePosition = ActorInput.TargetPosition;
            var direction = (mousePosition - position).normalized;

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gunTransform.rotation = Quaternion.Euler(0, 0, angle);

            var gunPosition = position + Quaternion.Euler(0.4f, 0, angle) * Vector3.right * gunDistance;
            gunPosition.z = -1;
            gunPosition.y -= 0.2f;
            gunTransform.position = gunPosition;
            FlipGun(angle);
        }

        private void FlipGun(float angle)
        {
            gunTransform.localScale = angle is > 90 or < -90
                ? new Vector3(1, -1, 1)
                : new Vector3(1, 1, 1);
        }
    }
}