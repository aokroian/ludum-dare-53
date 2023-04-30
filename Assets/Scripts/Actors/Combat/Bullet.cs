using UnityEngine;

namespace Actors.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speedBullet;
        [SerializeField] private GameObject particle;

        void Update()
        {
            transform.Translate(Vector2.right * (speedBullet * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var position = transform.position;
            var hit = Physics2D.Raycast(position, other.transform.position - position);
            Vector3 normal = hit.normal;
            var spawned = Instantiate(particle, hit.point, Quaternion.identity);
            spawned.transform.forward = normal;
            Destroy(gameObject);
        }
    }
}

