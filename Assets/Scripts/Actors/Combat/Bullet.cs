using UnityEngine;

namespace Actors.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speedBullet;
        
        void Update()
        {
            transform.Translate(Vector2.right * (speedBullet * Time.deltaTime));
        }
    }
}