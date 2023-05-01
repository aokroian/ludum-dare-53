using Actors.Upgrades;
using Sounds;
using UnityEngine;

namespace Actors.Combat
{
    public class Gun : MonoBehaviour, IDynamicStatsReceiver
    {
        [field: SerializeField] public GunTypes GunType { get; private set; }
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;

        [Space]
        [SerializeField] private float shootRate = 1;
        [SerializeField] private int bulletsPerShotCount = 1;
        [SerializeField] private float zAngleBetweenBullets = 8f;

        private float _shootRateTimer;
        private Vector3 _initialScale;

        private ActorGunSystem _gunSystem;
        private DynamicActorStats _dynamicActorStats;

        private float _currentShootRate;
        private float _currentBulletsPerShotCount;

        private void Awake()
        {
            _dynamicActorStats = GetComponentInParent<DynamicActorStats>();
            _initialScale = transform.localScale;
            _gunSystem = GetComponentInParent<ActorGunSystem>();

            if (_dynamicActorStats != null)
                _dynamicActorStats.AddReceiver(this);
        }

        private void OnDestroy()
        {
            if (_dynamicActorStats != null)
                _dynamicActorStats.RemoveReceiver(this);
        }

        private void Update()
        {
            _shootRateTimer -= Time.deltaTime;
        }

        public void ApplyDynamicStats(ActorStatsSo actorStatsSo)
        {
            _currentShootRate = shootRate + actorStatsSo.addedShootRate;
            _currentBulletsPerShotCount = bulletsPerShotCount + actorStatsSo.addedBulletsPerShotCount;
        }

        public void Fire()
        {
            if (_shootRateTimer <= 0)
            {
                var fullAngle = _currentBulletsPerShotCount > 1
                    ? _currentBulletsPerShotCount * zAngleBetweenBullets
                    : 0;

                var initialAngle = -(fullAngle / 2);
                for (var i = 0; i < _currentBulletsPerShotCount; i++)
                {
                    var bulletRotation = Quaternion.Euler(
                        0,
                        0,
                        transform.rotation.eulerAngles.z + initialAngle + i * zAngleBetweenBullets);
                    var spawnedBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);
                    spawnedBullet.ownerActor = _gunSystem.transform;
                    SoundSystem.GunShotSound(this);
                }

                _shootRateTimer = 1 / _currentShootRate;
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