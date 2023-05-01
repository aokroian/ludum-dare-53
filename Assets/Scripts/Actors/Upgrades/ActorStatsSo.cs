using System;
using UnityEngine;

namespace Actors.Upgrades
{
    [CreateAssetMenu(fileName = "ActorStats", menuName = "LD53/ActorStats", order = 4)]
    public class ActorStatsSo : ScriptableObject
    {
        public event Action OnValidateEvent;

        private void OnValidate()
        {
            OnValidateEvent?.Invoke();
        }

        [Header("Physics")]
        public float addedMovementSpeed;
        public float addedScaleModifier;

        [Header("Health")]
        public int addedMaxHealth;

        [Header("Combat")]
        public float addedShootRate;
        public float addedBulletsSpeed;
        public int addedBulletsDamage;
        public float addedBulletsScale;
        public int addedBulletsPerShotCount;
        public int addedBulletsPiercingCount;
    }
}