using UnityEngine;

namespace Actors.Combat
{
    public class DealDamageByTouch : MonoBehaviour
    {
        [SerializeField] private Collider2D damageCollider;
        [SerializeField] private float cooldownBetweenDamage = 0.4f;
        [SerializeField] private int damage = 10;

        public Transform ownerActor;

        private float _previousDamageTime;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.IsChildOf(ownerActor) ||
                !other.CompareTag("Enemy") || !other.CompareTag("Player"))
            {
                return;
            }

            var actorHealth = other.GetComponentInChildren<ActorHealth>();
            if (actorHealth == null || Time.time - _previousDamageTime <= cooldownBetweenDamage)
                return;
            actorHealth.TakeDamage(damage);
            _previousDamageTime = Time.time;
        }
    }
}