using Actors.ActorSystems;
using Actors.Upgrades;
using Sounds;
using UnityEngine;

namespace Actors.Combat
{
    public class Bullet : MonoBehaviour, IDynamicStatsReceiver
    {
        [field: SerializeField] public BulletTypes BulletType { get; private set; }
        [SerializeField] private GameObject particlesPrefab;
        [SerializeField] [Range(0.1f, 30f)] private float bulletSpeed = 10f;
        [SerializeField] [Range(1, 1000)] private int bulletDamage = 30;
        [SerializeField] [Range(0, 4)] private int bulletPiercingCount;

        [HideInInspector] public Transform ownerActor;

        public DynamicActorStats dynamicActorStats;

        private float _defaultBulletScale;
        private float _currentBulletSpeed;
        private int _currentBulletDamage;
        private float _currentBulletScale;
        private float _currentBulletPiercingCount;

        public void Init()
        {
            _defaultBulletScale = transform.localScale.x;
            _currentBulletScale = _defaultBulletScale;
            _currentBulletSpeed = bulletSpeed;
            _currentBulletDamage = bulletDamage;
            _currentBulletPiercingCount = bulletPiercingCount;

            if (dynamicActorStats != null)
                dynamicActorStats.AddReceiver(this);
        }

        private void OnDestroy()
        {
            if (dynamicActorStats != null)
                dynamicActorStats.RemoveReceiver(this);
        }

        public void ApplyDynamicStats(ActorStatsSo actorStatsSo)
        {
            _currentBulletDamage = bulletDamage + actorStatsSo.addedBulletsDamage;
            _currentBulletSpeed = bulletSpeed + actorStatsSo.addedBulletsSpeed;
            _currentBulletScale = _defaultBulletScale + actorStatsSo.addedBulletsScale;
            _currentBulletPiercingCount = bulletPiercingCount + actorStatsSo.addedBulletsPiercingCount;

            transform.localScale = Vector3.one * _currentBulletScale;
        }

        private void Update()
        {
            transform.Translate(Vector2.right * (_currentBulletSpeed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.IsChildOf(ownerActor) ||
                other.CompareTag("AI_Walk_Area") ||
                other.CompareTag("Damage_Collider") ||
                other.CompareTag("Fit_Player_To_Door_Area"))
            {
                return;
            }

            if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            {
                var actorHealth = other.GetComponentInChildren<ActorHealth>();
                if (actorHealth != null)
                    actorHealth.TakeDamage(_currentBulletDamage);
                SoundSystem.BulletHitSound(this);
                if (_currentBulletPiercingCount <= 0)
                    Destroy(gameObject);
                _currentBulletPiercingCount--;
            }
            else
                OnHitObstacle(other);
        }

        private void OnHitObstacle(Collider2D other)
        {
            var position = transform.position;
            var hit = Physics2D.Raycast(position, other.transform.position - position);
            Vector3 normal = hit.normal;
            var spawned = Instantiate(particlesPrefab, hit.point, Quaternion.identity);
            spawned.transform.localScale = Vector3.one * _currentBulletScale;
            spawned.transform.forward = normal;
            SoundSystem.BulletHitSound(this);
            Destroy(gameObject);
        }
    }
}