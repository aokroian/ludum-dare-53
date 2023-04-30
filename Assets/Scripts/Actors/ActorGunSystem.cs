using System;
using Actors.Combat;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actors
{
    public class ActorGunSystem : ActorSystem
    {
        [SerializeField] private float gunDistance = 0.6f;
        [SerializeField] private Gun currentActiveGun;

        public event Action<Gun> OnActiveGunChanged;

        private GunsConfigSo _gunsConfig;

        private bool _isGunSpawned;

        protected override void Awake()
        {
            base.Awake();
            _gunsConfig = Resources.Load<GunsConfigSo>("GunsConfig");

            ChangeActiveGun(GunTypes.Pistol);
        }

        private void Update()
        {
            if (!_isGunSpawned)
                return;
            RotateGunAroundPlayer();
            if (ActorInput.Fire)
                currentActiveGun.Fire();

            // temp code jus for testing 
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ChangeActiveGun(GunTypes.Pistol);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                ChangeActiveGun(GunTypes.Shotgun);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                ChangeActiveGun(GunTypes.Rifle);
        }

        private void ChangeActiveGun(GunTypes gunType)
        {
            if (currentActiveGun != null && currentActiveGun.GunType == gunType)
                return;
            var gun = _gunsConfig.GetGunPrefab(gunType);
            if (gun == null)
            {
                Debug.LogError($"Gun with type {gunType} not found");
                return;
            }

            if (currentActiveGun != null)
                Destroy(currentActiveGun.gameObject);

            var spawnedGun = Instantiate(gun, transform);

            currentActiveGun = spawnedGun;
            _isGunSpawned = true;
            OnActiveGunChanged?.Invoke(currentActiveGun);
        }


        private void RotateGunAroundPlayer()
        {
            var position = transform.position;
            var mousePosition = ActorInput.Look;
            var direction = (mousePosition - position).normalized;

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            currentActiveGun.transform.rotation = Quaternion.Euler(0, 0, angle);

            var gunPosition = position + Quaternion.Euler(0.4f, 0, angle) * Vector3.right * gunDistance;
            gunPosition.z = -1;
            gunPosition.y -= 0.2f;
            currentActiveGun.transform.position = gunPosition;
            currentActiveGun.FlipSprite(angle);
        }
    }
}