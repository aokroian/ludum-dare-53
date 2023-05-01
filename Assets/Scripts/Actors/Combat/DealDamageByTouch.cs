using DG.Tweening;
using UnityEngine;

namespace Actors.Combat
{
    public class DealDamageByTouch : MonoBehaviour
    {
        [SerializeField] private float cooldownBetweenDamage = 2f;
        [SerializeField] private int damage = 1;

        public Transform ownerActor;
        private string OwnerActorTag => ownerActor.tag;

        private float _damageTimer;

        private void Update()
        {
            _damageTimer += Time.time;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TryToDealDamage(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TryToDealDamage(other);
        }

        private void TryToDealDamage(Collider2D other)
        {
            if (other.transform.IsChildOf(ownerActor) || other.CompareTag(OwnerActorTag))
                return;

            if (_damageTimer > cooldownBetweenDamage)
            {
                var actorHealth = other.GetComponentInChildren<ActorHealth>();
                if (actorHealth == null)
                    return;
                actorHealth.TakeDamage(damage);
                Debug.Log("damage");
                PushVictim(other.transform);
                _damageTimer = 0;
            }
        }

        private void PushVictim(Transform victim)
        {
            var victimPos = victim.position;
            var dir = victimPos - transform.position;
            var targetPos = victimPos + dir * .8f;
            victim.DOMove(targetPos, 0.25f).SetEase(Ease.OutElastic);
        }
    }
}