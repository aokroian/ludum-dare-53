using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Actors.Combat
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform muzzle;
        [SerializeField][Range(0.01f,30f)] private float shootRate;
        [SerializeField] private GameObject bullet;
        [SerializeField][Range(1,7)] private int quantityShot = 3;
        [SerializeField][Range(0, 360)] private float totalAngle = 30f;

        private float _shootRateTimer;
        
        private void Update()
        {
            _shootRateTimer -= Time.deltaTime;
        }

        public void Fire()
        {
            if (_shootRateTimer <= 0)
            {
                float angleBetweenBullets = 0;
                if (quantityShot > 1)
                {
                    angleBetweenBullets = totalAngle / (quantityShot - 1);
                }

                var initialAngle = -(totalAngle / 2);
                for (var i = 0; i < quantityShot; i++)
                {
                    var bulletRotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + initialAngle + (i * angleBetweenBullets));
                    Instantiate(bullet, muzzle.position, bulletRotation);
                }
                _shootRateTimer = 1 / shootRate;
            }
        }
    }
}