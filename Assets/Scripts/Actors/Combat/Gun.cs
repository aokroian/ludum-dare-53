using Actors.InputThings;
using UnityEngine;

namespace Actors.Combat
{
    public class Gun : MonoBehaviour
    {
        [field: SerializeField] public GunTypes GunType { get; private set; }
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
        [Space]
        [SerializeField] [Range(0.01f, 30f)] private float shootRate;
        [SerializeField] [Range(1, 7)] private int bulletsPerShotCount = 1;
        [SerializeField] [Range(0, 360)] private float bulletsDispersionAngle;

        private float _shootRateTimer;
        private Vector3 _initialScale;

        private ActorGunSystem _gunSystem;

        private void Awake()
        {
            _initialScale = transform.localScale;
            _gunSystem = GetComponentInParent<ActorGunSystem>();
        }

        private void Update()
        {
            _shootRateTimer -= Time.deltaTime;
        }

        public void Fire()
        {
            if (_shootRateTimer <= 0)
            {
                float angleBetweenBullets = 0;
                if (bulletsPerShotCount > 1)
                {
                    angleBetweenBullets = bulletsDispersionAngle / (bulletsPerShotCount - 1);
                }

                var initialAngle = -(bulletsDispersionAngle / 2);
                for (var i = 0; i < bulletsPerShotCount; i++)
                {
                    var bulletRotation = Quaternion.Euler(
                        0,
                        0,
                        transform.rotation.eulerAngles.z + initialAngle + (i * angleBetweenBullets));
                    var spawnedBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);
                    spawnedBullet.ownerActor = _gunSystem.transform;
                }

                _shootRateTimer = 1 / shootRate;
            }
        }

        public void FlipSprite(float angleZ)
        {
            transform.localScale = angleZ is > 90 or < -90
                ? new Vector3(_initialScale.x, -_initialScale.y, _initialScale.z)
                : _initialScale;
        }
    }
}