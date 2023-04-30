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


        private float _shootRateTimer;
        
        private void Update()
        {
            _shootRateTimer -= Time.deltaTime;
        }

        public void Fire()
        {
            if (_shootRateTimer <= 0)
            {
                Instantiate(bullet, muzzle.position, transform.rotation);
                _shootRateTimer = 1 / shootRate;
                
                
            }
        }
    }
}