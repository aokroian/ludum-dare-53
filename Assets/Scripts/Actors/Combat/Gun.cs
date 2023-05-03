using System;
using Actors.ActorSystems;
using Actors.Upgrades;
using Sounds;
using UnityEngine;

namespace Actors.Combat
{
    public class Gun : MonoBehaviour, IActorStatsReceiver
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
        private ActorStatsController _actorStatsController;

        private float _currentShootRate;
        private float _currentBulletsPerShotCount;

        public event Action OnFire;

        private void Awake()
        {
            _currentShootRate = shootRate;
            _currentBulletsPerShotCount = bulletsPerShotCount;

            _actorStatsController = GetComponentInParent<ActorStatsController>();
            _initialScale = transform.localScale;
            _gunSystem = GetComponentInParent<ActorGunSystem>();

            if (_actorStatsController != null)
                _actorStatsController.AddReceiver(this);
        }

        private void OnDestroy()
        {
            if (_actorStatsController != null)
                _actorStatsController.RemoveReceiver(this);
        }

        private void Update()
        {
            _shootRateTimer -= Time.deltaTime;
        }

        public void ApplyDynamicStats(ActorStatsSo actorStatsSo)
        {
            _currentShootRate = shootRate + actorStatsSo.addedShootRate;
            if (_currentShootRate <= 0)
                _currentShootRate = 0.1f;
            _currentBulletsPerShotCount = bulletsPerShotCount + actorStatsSo.addedBulletsPerShotCount;
            if (_currentBulletsPerShotCount < 1)
                _currentBulletsPerShotCount = 1;
        }

        public void Fire()
        {
            if (_shootRateTimer <= 0)
            {
                var fullAngle = _currentBulletsPerShotCount > 1
                    ? _currentBulletsPerShotCount * zAngleBetweenBullets
                    : 0;

                var initialAngle = -(fullAngle / 2);
                OnFire?.Invoke();
                SoundSystem.GunShotSound(this);
                for (var i = 0; i < _currentBulletsPerShotCount; i++)
                {
                    var bulletRotation = Quaternion.Euler(
                        0,
                        0,
                        transform.rotation.eulerAngles.z + initialAngle + i * zAngleBetweenBullets);
                    var spawnedBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletRotation);
                    spawnedBullet.actorStatsController = _actorStatsController;
                    spawnedBullet.Init();
                    spawnedBullet.ownerActor = _gunSystem.transform;
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