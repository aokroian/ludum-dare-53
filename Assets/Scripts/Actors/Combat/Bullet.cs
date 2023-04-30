using UnityEngine;

namespace Actors.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speedBullet;
        private float _timer;
        private const float TimeDestroy = 2f;
        
        void Update()
        {
            _timer += Time.deltaTime;
            transform.Translate(Vector2.right * (speedBullet * Time.deltaTime));
            if (!(_timer >= TimeDestroy)) return;
            Destroy(gameObject);
            _timer = 0;
        }
    }
}