using System;
using Sounds;
using UnityEngine;

namespace Actors
{
    public class ActorHealth : ActorSystem
    {
        public float invincibilityTime = 0f;
        
        public bool isDeathSound;
        public bool isDamageSound;
        public bool isHealSound;

        [field: SerializeField] public int MaxHealth { get; private set; } = 100;
        [field: SerializeField] public int Health { get; private set; }
        public bool IsDead => Health <= 0;

        public event Action<ActorHealth> OnDeath;
        private event Action<ActorHealth> OnRevive;

        public event Action<int> OnHealthChanged;
        public event Action<int> OnHeal;
        public event Action<int> OnDamageTaken;

        private float _invincibilityEndTime;

        protected override void Awake()
        {
            base.Awake();
            Health = MaxHealth;
        }

        public void Heal(int amount)
        {
            if (amount <= 0)
                return;
            var newHealth = Health + amount;

            if (IsDead && newHealth > 0)
                OnRevive?.Invoke(this);

            Health = newHealth;
            if (Health > MaxHealth)
                Health = MaxHealth;

            OnHealthChanged?.Invoke(Health);
            OnHeal?.Invoke(amount);
            if (isHealSound)
                SoundSystem.ActorHealSound(this);
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0 || _invincibilityEndTime > Time.time)
                return;
            
            _invincibilityEndTime = Time.time + invincibilityTime;

            Health -= damage;
            if (Health < 0)
                Health = 0;
            
            if (IsDead)
            {
                if (isDeathSound)
                    SoundSystem.ActorDeathSound(this);
                OnDeath?.Invoke(this);
            }


            OnHealthChanged?.Invoke(Health);
            OnDamageTaken?.Invoke(damage);
            if (isDamageSound)
                SoundSystem.ActorDamageSound(this);
        }
    }
}