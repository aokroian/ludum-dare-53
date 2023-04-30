using Actors.Combat;
using UnityEngine;

namespace Actors
{
    public class ActorScopeSystem : ActorSystem
    {
        [SerializeField] private Transform scopeTransform;

        private GunsConfigSo _gunsConfig;
        private ActorGunSystem _actorGunSystem;

        private bool _isScopeSpawned;

        protected override void Awake()
        {
            base.Awake();
            _gunsConfig = Resources.Load<GunsConfigSo>("GunsConfig");

            var root = transform.root;
            _actorGunSystem = root.GetComponentInChildren<ActorGunSystem>();
            if (_actorGunSystem != null)
                _actorGunSystem.OnActiveGunChanged += OnGunChanged;
        }

        private void Update()
        {
            if (_isScopeSpawned)
                scopeTransform.transform.position = ActorInput.Look;
        }

        private void OnGunChanged(Gun gun)
        {
            var scopePrefab = _gunsConfig.GetScopePrefab(gun.GunType);
            if (scopePrefab == null)
            {
                Debug.LogError($"Scope with type {gun.GunType} not found");
                return;
            }

            if (scopeTransform != null)
                Destroy(scopeTransform.gameObject);
            var spawnedScope = Instantiate(scopePrefab, transform);
            scopeTransform = spawnedScope.transform;
            _isScopeSpawned = true;
        }
    }
}