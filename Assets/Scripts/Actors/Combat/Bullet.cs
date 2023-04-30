using UnityEngine;

namespace Actors.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject particlesPrefab;
        [SerializeField] [Range(0.1f, 30f)] private float bulletSpeed = 10f;
        [SerializeField] [Range(1, 1000)] private int bulletDamage = 30;

        public Transform ownerActor;

        private void Update()
        {
            transform.Translate(Vector2.right * (bulletSpeed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.IsChildOf(ownerActor))
                return;
            if (other.CompareTag("Actor"))
            {
                var actorHealth = other.GetComponentInChildren<ActorHealth>();
                if (actorHealth != null)
                    actorHealth.TakeDamage(bulletDamage);
                Destroy(gameObject);
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
            spawned.transform.forward = normal;
            Destroy(gameObject);
        }
    }
}